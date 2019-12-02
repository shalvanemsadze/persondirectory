using AutoMapper;
using PersonDirectory.Configuration;
using PersonDirectory.Data;
using PersonDirectory.Repository;
using PersonDirectory.Shared.Helper_Types.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PersonDirectory.Service.BusinessLogic.Base
{
    public class BaseService
    {
        PersonDirectoryContext _context = null;
        private UnitOfWork _uow = null;
        protected Mapper Mapper = AutoMapperConfiguration.Mapper;

        protected UnitOfWork Uow
        {
            get
            {
                if (_uow == null)
                {
                    _uow = new UnitOfWork(_context);
                }
                return _uow;
            }
        }

        public BaseService(PersonDirectoryContext context)
        {
            _context = context;
        }

        protected void ValidateModelState(object instance)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(instance);
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
