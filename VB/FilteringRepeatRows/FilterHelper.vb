Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.XtraGrid.Views.Grid
Imports System.Data

Namespace FilteringRepeatRows
	Public Class FilterHelper
		Private _view As GridView
		Private _fields As List(Of String)

		Public Property Fields() As List(Of String)
			Get
				Return _fields
			End Get
			Set(ByVal value As List(Of String))
				If value Is _fields Then
					Return
				End If
				_fields = value
				_view.RefreshData()
			End Set
		End Property

		Public Sub New(ByVal view As GridView)
			_view = view
			AddHandler _view.CustomRowFilter, AddressOf _view_CustomRowFilter
		End Sub

		Private Sub _view_CustomRowFilter(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowFilterEventArgs)
			Dim view As GridView = TryCast(sender, GridView)
			Dim source As DataView = CType(view.DataSource, DataView)
			e.Visible = Not ContainsRow(source, e.ListSourceRow)
			e.Handled = True
		End Sub

		Private Function ContainsRow(ByVal source As DataView, ByVal listSourceRow As Integer) As Boolean
			For i As Integer = 0 To listSourceRow - 1
				Dim equal As Boolean = True
				For Each column As DataColumn In source.Table.Columns
					If CheckField(column.ColumnName) AndAlso (Not source.Table.Rows(listSourceRow)(column).Equals(source.Table.Rows(i)(column))) Then
						equal = False
						Exit For
					End If
				Next column
				If equal Then
					Return True
				End If
			Next i
			Return False
		End Function

		Private Function CheckField(ByVal columnName As String) As Boolean
			Return _fields Is Nothing OrElse _fields.Contains(columnName)
		End Function
	End Class
End Namespace
