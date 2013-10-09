namespace VinaSale.Box.Domain.Models
{
    using System;

    using FluentNHibernate.Data;

    using VinaSale.Core.Domain.Models;

    [Serializable]
    public sealed class Comment : Entity
    {
        public string Content { get; set; }

        public User CommentUser { get; set; }

        public SaleInfo ConmmentSaleInfo { get; set; }
    }
}
