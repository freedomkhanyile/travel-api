using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Travel.Data.Access.DAL;
using Microsoft.Extensions.Logging;
namespace Travel.Helpers
{
    public class ActionTransactionHelper : IActionTransactionHelper
    {
        private IUnitOfWork _unitOfWork;
        private ITransaction _transaction;
        private readonly ILogger _log;

        public ActionTransactionHelper(IUnitOfWork unitOfWork, ILogger log)
        {
            _unitOfWork = unitOfWork;
            _log = log;
        }
        private bool TransactionHandled { get; set; }
        private bool SessionClosed { get; set; }

        public void BeginTransaction()
        {
            _transaction = _unitOfWork.BeginTransaction();
        }

        public void EndTransaction(ActionExecutedContext filterContext)
        {
          if(_transaction == null) throw new NotSupportedException();
          if (filterContext.Exception == null)
          {
              _unitOfWork.Commit();
              _transaction.Commit();
          }
          else
          {
              try
              { 
                  _transaction.Rollback();
              }
              catch (Exception e)
              {
                  throw new AggregateException(filterContext.Exception, e);
              }
          }

          TransactionHandled = true;
        }

        public void CloseSession()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }

            if (_unitOfWork != null)
            {
                _unitOfWork.Dispose();
                _unitOfWork = null;
            }

            SessionClosed = true;
        }
    }
}
