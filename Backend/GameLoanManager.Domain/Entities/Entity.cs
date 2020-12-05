using System;
using System.ComponentModel.DataAnnotations;

namespace GameLoanManager.Domain.Entities
{
    public class Entity
    {
        [Key]
        public long Id { get; set; }

        private DateTime _createAt;
        public DateTime CreateAt
        {
            get { return _createAt; }
            set { _createAt = value.Equals(DateTime.MinValue) ? DateTime.UtcNow : value; }
        }
        public DateTime? UpdateAt { get; set; }
    }
}