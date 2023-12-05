using Application.Services.Cryptography;
using Application.Services.Token;
using AutoMapper;
using Communication.Requests;
using Communication.Responses;
using Domain.Repositories;
using Exceptions.ExceptionBase;
using Infrastructure.RepositoryAccess.UnitOfWork;
using Serilog;

namespace Application.UseCases.User.Create;

public class CreateUseCase : ICreateUseCase
{
    private readonly IUserWriteOnlyRepository _repository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly Encryptor _encryptor;
    private readonly TokenService _tokenService;

    public CreateUseCase(IUserWriteOnlyRepository repository, IMapper mapper, IUnitOfWork unitOfWork, Encryptor encryptor, TokenService tokenService)
    {
        _repository = repository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _encryptor = encryptor;
        _tokenService = tokenService;
    }

    public async Task<UserResponse> Execute(UserRequest request)
    {
        Validate(request);

        var entity = _mapper.Map<Domain.Entities.User>(request);
        entity.Password = _encryptor.Encrypt(request.Password);

        await _repository.Add(entity);

        await _unitOfWork.Commit();

        var token = _tokenService.GenerateToken(entity.Email);

        return new UserResponse()
        {
            Token = token
        };
    }

    private static void Validate(UserRequest request)
    {
        var validator = new CreateValidator();
        var result = validator.Validate(request);
        
        if (!result.IsValid)
        {
            var errorMessages = result.Errors
                .Select(error => error.ErrorMessage).ToList();
            
            Log.ForContext("UserName", request.Name)
                .Error($"{errorMessages.FirstOrDefault()}");

            throw new ValidationErrorsException(errorMessages);
        }
    }
}