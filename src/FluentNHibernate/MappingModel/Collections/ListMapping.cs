﻿using System;
using System.Linq.Expressions;
using FluentNHibernate.Utils;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.Collections
{
    [Serializable]
    public class ListMapping : CollectionMappingBase, IIndexedCollectionMapping
    {
        private readonly AttributeStore<ListMapping> attributes;

        public IIndexMapping Index
        {
            get { return attributes.Get(x => x.Index); }
            set { attributes.Set(x => x.Index, value); }
        }

        public ListMapping()
            : this(new AttributeStore())
        {}

        public ListMapping(AttributeStore underlyingStore)
            : base(underlyingStore)
        {
            attributes = new AttributeStore<ListMapping>(underlyingStore);
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessList(this);

            if(Index != null)
                visitor.Visit(Index);

            base.AcceptVisitor(visitor);
        }

    	public override string OrderBy
    	{
			get { return null; }
			set { /* no-op */ }
    	}

        public new bool IsSpecified(string property)
        {
            return attributes.IsSpecified(property);
        }

        public bool HasValue<TResult>(Expression<Func<ListMapping, TResult>> property)
        {
            return attributes.HasValue(property);
        }

        public void SetDefaultValue<TResult>(Expression<Func<ListMapping, TResult>> property, TResult value)
        {
            attributes.SetDefault(property, value);
        }

        public bool Equals(ListMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && Equals(other.attributes, attributes);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as ListMapping);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                {
                    return (base.GetHashCode() * 397) ^ (attributes != null ? attributes.GetHashCode() : 0);
                }
            }
        }
    }
}
