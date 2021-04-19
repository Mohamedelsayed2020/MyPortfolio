using Core.Interfaces;
using Infrastructure.Repositry;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        private readonly DataContext _context;
        private IGenericRepositry<T> _entity;
        public UnitOfWork(DataContext context)
        {
            _context = context;
        }
        public IGenericRepositry<T> Entity 
        {
            get
            {
                return _entity ?? (_entity = new GenericRepositry<T>(_context));
            }
        }
       
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
