using System;
using System.Collections.Generic;
using System.Text;

namespace DataDevelop.Data
{
	public enum OrderType
	{
		None = 0, Ascending, Descending
	}

	////class ColumnParameters : ICloneable
	////{
	////    private bool output = true;
	////    private Column column;
	////    private OrderType orderType = OrderType.None;

	////    internal ColumnParameters(Column column)
	////    {
	////        this.column = column;
	////    }

	////    public bool Output
	////    {
	////        get { return output; }
	////        set
	////        {
	////            if (!value && column.InPrimaryKey) {
	////                throw new InvalidOperationException("All columns in primary key must be outputted");
	////            }
	////            output = value;
	////        }
	////    }

	////    public string ColumnName
	////    {
	////        get { return column.Name; }
	////    }

	////    public void Clear()
	////    {
	////        this.orderType = OrderType.None;
	////    }

	////    object ICloneable.Clone()
	////    {
	////        return Clone();
	////    }

	////    public ColumnParameters Clone()
	////    {
	////        ColumnParameters parameters = new ColumnParameters(this.column);
	////        parameters.output = this.output;
	////        parameters.orderType = this.orderType;
	////        return parameters;
	////    }
	////}
}
