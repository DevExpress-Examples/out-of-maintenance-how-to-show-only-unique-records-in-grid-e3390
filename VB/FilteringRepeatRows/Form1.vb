Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms

Namespace FilteringRepeatRows
	Partial Public Class Form1
		Inherits Form
		Private filterFields(1) As List(Of String)
		Private helper As FilterHelper

		Public Sub New()
			InitializeComponent()
			Dim fields As New List(Of String)()
			fields.Add("Name")
			fields.Add("Company")
			fields.Add("Address")
			filterFields(0) = Nothing
			filterFields(1) = fields
			helper = New FilterHelper(gridView1)
			radioGroup1.SelectedIndex = 0
			gridControl1.DataSource = CreateTable(20)
		End Sub

		Private Function CreateTable(ByVal rowCount As Integer) As DataTable
			Dim table As New DataTable()
			table.Columns.Add("Name")
			table.Columns.Add("Company")
			table.Columns.Add("Age", GetType(Integer))
			table.Columns.Add("Address")
			Dim repeatCount As Integer = 3
			For i As Integer = 0 To rowCount - 1
				For j As Integer = 0 To repeatCount - 1
					table.Rows.Add(New Object() { String.Format("Name{0}", i), String.Format("Company{0}", i), 25 + i * repeatCount + j, String.Format("Address{0}", i) })
				Next j
			Next i
			Return table
		End Function

		Private Sub radioGroup1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles radioGroup1.SelectedIndexChanged
			helper.Fields = filterFields(radioGroup1.SelectedIndex)
		End Sub
	End Class
End Namespace
