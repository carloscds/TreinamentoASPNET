using FluentValidation;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIBanco.InfraEstrutura.EF.Validators
{
    public class ValidatorFactory<TEntity> where TEntity : class
    {
        private static ConcurrentDictionary<string, IValidator> _validators = new ConcurrentDictionary<string, IValidator>();
        private readonly static object objLock = new object();
        public IValidator Create()
        {
            var validatorType = "APIBanco.InfraEstrutura.EF.Validation." + typeof(TEntity).Name + "Validator";
            if (_validators.TryGetValue(validatorType, out IValidator value))
            {
                return value;
            }
            else
            {
                lock (objLock)
                {
                    var validatorName = Type.GetType(validatorType);
                    if (validatorName == null) return null;
                    var newValidator = (IValidator)Activator.CreateInstance(validatorName);
                    _validators.TryAdd(validatorName.FullName, newValidator);
                    return newValidator;
                }
            }
        }
    }
}
