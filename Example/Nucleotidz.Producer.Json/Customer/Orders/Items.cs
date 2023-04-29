// ------------------------------------------------------------------------------
// <auto-generated>
//    Generated by avrogen, version 1.10.0.0
//    Changes to this file may cause incorrect behavior and will be lost if code
//    is regenerated
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Customer.Orders
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	using Avro;
	using Avro.Specific;
	
	public partial class Items : ISpecificRecord
	{
		public static Schema _SCHEMA = Avro.Schema.Parse("{\"type\":\"record\",\"name\":\"Items\",\"namespace\":\"Customer.Orders\",\"fields\":[{\"name\":\"" +
				"quantity\",\"type\":\"int\"},{\"name\":\"Name\",\"type\":\"string\"}]}");
		private int _quantity;
		private string _Name;
		public virtual Schema Schema
		{
			get
			{
				return Items._SCHEMA;
			}
		}
		public int quantity
		{
			get
			{
				return this._quantity;
			}
			set
			{
				this._quantity = value;
			}
		}
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				this._Name = value;
			}
		}
		public virtual object Get(int fieldPos)
		{
			switch (fieldPos)
			{
			case 0: return this.quantity;
			case 1: return this.Name;
			default: throw new AvroRuntimeException("Bad index " + fieldPos + " in Get()");
			};
		}
		public virtual void Put(int fieldPos, object fieldValue)
		{
			switch (fieldPos)
			{
			case 0: this.quantity = (System.Int32)fieldValue; break;
			case 1: this.Name = (System.String)fieldValue; break;
			default: throw new AvroRuntimeException("Bad index " + fieldPos + " in Put()");
			};
		}
	}
}
