
using PAP.DataAccess;
using PAP.Repositories;
using System.Collections.Generic;

namespace PAP.Business
{
    public class PaymentManager
    {
        private readonly PaymentRepository _repository;

        public PaymentManager()
        {
            _repository = new PaymentRepository();
        }

        public IEnumerable<Payment> GetAllPayments()
        {
            return _repository.GetAll();
        }

        public Payment GetPaymentById(int id)
        {
            return _repository.GetById(id);
        }

        public void AddPayment(Payment payment)
        {
            _repository.Add(payment);
        }

        public void UpdatePayment(Payment payment)
        {
            _repository.Update(payment);
        }

        public void DeletePayment(int id)
        {
            _repository.Delete(id);
        }
    }
}

