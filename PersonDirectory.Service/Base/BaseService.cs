using PersonDirectory.Data;
using PersonDirectory.Repository;
using PersonDirectory.Shared.Helper_Types.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PersonDirectory.Service
{
    public class BaseService
    {
        PersonDirectoryContext _context = null;
        private UnitOfWork _uow = null;
        public UnitOfWork Uow
        {
            get
            {
                if (this._uow == null)
                {
                    this._uow = new UnitOfWork(_context);
                }
                return this._uow;
            }
        }

        public BaseService(PersonDirectoryContext context)
        {
            _context = context;
        }

        protected void ValidateModelState(object instance)
        {
            var validationContext = new ValidationContext(instance);
            bool isValid = Validator.TryValidateObject(instance, validationContext, null, true);

            if (!isValid)
                throw new InvalidModelStateException();
        }

        protected void CheckNull(object param)
        {
            if (param == null)
                throw new Exception("Data not passed");
        }

        public void Dispose()
        {
            if (_uow != null)
            {
                _uow.Dispose();
                _uow = null;
            }
        }

    }
}
