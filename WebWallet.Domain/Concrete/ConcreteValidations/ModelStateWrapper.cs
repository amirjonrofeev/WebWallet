using System.Web.Mvc;
using WebWallet.Domain.Abstract;

namespace WebWallet.Domain.Concrete
{
    public class ModelStateWrapper : IValidationDictionary
    {
        private ModelStateDictionary _modelStateDicitionary;

        public ModelStateWrapper(ModelStateDictionary modelStateDictionary)
        {
            _modelStateDicitionary = modelStateDictionary;
        }

        public bool IsValid => _modelStateDicitionary.IsValid;

        public void AddError(string key, string errorMessage)
        {
            _modelStateDicitionary.AddModelError(key, errorMessage);
        }
    }
}
