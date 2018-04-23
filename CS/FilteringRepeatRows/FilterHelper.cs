using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraGrid.Views.Grid;
using System.Data;

namespace FilteringRepeatRows
{
    public class FilterHelper
    {
        GridView _view;
        List<string> _fields;

        public List<string> Fields
        {
            get
            {
                return _fields;
            }
            set
            {
                if (value == _fields) return;
                _fields = value;
                _view.RefreshData();
            }
        }
        
        public FilterHelper(GridView view)
        {
            _view = view;
            _view.CustomRowFilter += _view_CustomRowFilter;
        }

        void _view_CustomRowFilter(object sender, DevExpress.XtraGrid.Views.Base.RowFilterEventArgs e)
        {
            GridView view = sender as GridView;
            DataView source = (DataView)view.DataSource;
            e.Visible = !ContainsRow(source, e.ListSourceRow);
            e.Handled = true;
        }

        bool ContainsRow(DataView source, int listSourceRow)
        {
            for (int i = 0; i < listSourceRow; i++)
            {
                bool equal = true;
                foreach (DataColumn column in source.Table.Columns)
                {
                    if (CheckField(column.ColumnName) && !source.Table.Rows[listSourceRow][column].Equals(source.Table.Rows[i][column]))
                    {
                        equal = false;
                        break;
                    }
                }
                if (equal)
                    return true;
            }
            return false;
        }

        bool CheckField(string columnName)
        {
            return _fields == null || _fields.Contains(columnName);
        }
    }
}
