using AutoMapper;
using Communication.Requests;
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

    public CreateUseCase(IUserWriteOnlyRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(UserRequest request)
    {
        Validate(request);

        var entity = _mapper.Map<Domain.Entities.User>(request);

        await _repository.Add(entity);

        await _unitOfWork.Commit();
    }

    private static void Validate(UserRequest request)
    {
        var validator = new CreateValidator();
        var result = validator.Validate(request);
        
        if (!result.IsValid)
        {
            var errorMessages = result.Errors
                .Select(error => error.ErrorMessage).ToList();
            
            Log.ForContext("UserName","mts")
                .ForContext("UserId",1)
                .Error($"{errorMessages}");

            throw new ValidationErrorsException(errorMessages);
        }
    }
}