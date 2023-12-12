﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Exceptions {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ErrorMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ErrorMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Exceptions.ErrorMessages", typeof(ErrorMessages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to O cnpj deve ser informado..
        /// </summary>
        public static string CNPJ_EM_BRANCO {
            get {
                return ResourceManager.GetString("CNPJ_EM_BRANCO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to O cnpj não está no formato correto: XX.XXX.XXX/YYYY-ZZ.
        /// </summary>
        public static string CNPJ_INVALIDO {
            get {
                return ResourceManager.GetString("CNPJ_INVALIDO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to O e-mail deve ser preenchido..
        /// </summary>
        public static string EMAIL_EM_BRANCO {
            get {
                return ResourceManager.GetString("EMAIL_EM_BRANCO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Este não é um e-mail válido..
        /// </summary>
        public static string EMAIL_INVALIDO {
            get {
                return ResourceManager.GetString("EMAIL_INVALIDO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Esse email já está cadastrado..
        /// </summary>
        public static string EMAIL_REGISTRADO {
            get {
                return ResourceManager.GetString("EMAIL_REGISTRADO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Erro desconhecido..
        /// </summary>
        public static string ERRO_DESCONHECIDO {
            get {
                return ResourceManager.GetString("ERRO_DESCONHECIDO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to O nome deve ser preenchido..
        /// </summary>
        public static string NOME_EM_BRANCO {
            get {
                return ResourceManager.GetString("NOME_EM_BRANCO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A senha deve conter no minímo 6 caracteres.
        /// </summary>
        public static string SENHA_CURTA {
            get {
                return ResourceManager.GetString("SENHA_CURTA", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A senha deve ser preenchida..
        /// </summary>
        public static string SENHA_EM_BRANCO {
            get {
                return ResourceManager.GetString("SENHA_EM_BRANCO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to O telefone deve ser informado..
        /// </summary>
        public static string TELEFONE_EM_BRANCO {
            get {
                return ResourceManager.GetString("TELEFONE_EM_BRANCO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to O telefone não está no formato correto: XX X XXXX-XXXX.
        /// </summary>
        public static string TELEFONE_INVALIDO {
            get {
                return ResourceManager.GetString("TELEFONE_INVALIDO", resourceCulture);
            }
        }
    }
}
