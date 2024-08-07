using APIBanco.Domain.Models;
using APIBanco.InfraEstrutura.EF.Validators;
using FluentValidation;

namespace APIBanco.Core.Abstract
{
    public class ServiceBase<TEntity> where TEntity : class
    {
        internal readonly IList<ModelErrors> _errors;
        private readonly IValidator validator;

        public IList<ModelErrors> GetErrors() => _errors;

        public ServiceBase()
        {
            validator = new ValidatorFactory<TEntity>().Create();
            _errors = new List<ModelErrors>();
        }
        public bool ValidateModel(TEntity entity)
        {
            var validatorContext = new ValidationContext<TEntity>(entity);
            var result = validator.Validate(validatorContext);
            if (result.Errors.Count > 0)
            {
                foreach (var error in result.Errors)
                {
                    _errors.Add(new ModelErrors { Mensagem = error.ErrorMessage });
                }
                return false;
            }
            return true;
        }
    }
}
