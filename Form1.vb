
Imports System.Drawing.Printing

Public Class Form1

    Dim NamaToko As String = "212 Mart Aceh"
    Dim Alamat As String = "Jl. Mata Ie, Keutapang Dua"
    Dim Kota As String = "Banda Aceh"

    Dim Trans As String = "PJ20060001"
    Dim Tgl As String = Format(Now, "yyyy-MM-dd HH:mm:ss")

    Dim arrWidth() As Integer
    Dim arrFormat() As StringFormat

    Dim Header1() As Integer
    Dim Header2() As StringFormat

    Dim Footer1() As Integer
    Dim Footer2() As StringFormat

    Dim c As New PrintingFormat

    Dim SubTotal As Double


    Sub TotalPenjualan()
        Dim total As Double

        For Each i As DataGridViewRow In Me.DataGridView1.Rows
            total += i.Cells(3).Value
            txtTotal.Text = FormatNumber(total, 0)
        Next
    End Sub

    Private Sub DataGridView1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        If e.ColumnIndex = 2 Then
            Me.DataGridView1.Rows(e.RowIndex).Cells(3).Value = Me.DataGridView1.Rows(e.RowIndex).Cells(1).Value * Me.DataGridView1.Rows(e.RowIndex).Cells(2).Value
            TotalPenjualan()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Printer.NewPrint()
        Printer.Print(" ")

        Header1 = {250, 0} : Header2 = {c.MidCenter}

        Printer.SetFont("Courier New", 11, FontStyle.Bold)
        Printer.Print(NamaToko, Header1, Header2)

        Printer.SetFont("Courier New", 8, FontStyle.Regular)
        Printer.Print(Alamat, {250}, 0)

        Printer.SetFont("Courier New", 8, FontStyle.Regular)
        Printer.Print(Kota, {250}, 0)

        Printer.Print("-----------------------------------")
        Printer.Print("Transaksi : " & Trans)
        Printer.Print("Tgl       : " & Tgl)
        Printer.Print("Kasir     : Sandi")
        Printer.Print("-----------------------------------")

        Printer.SetFont("Courier New", 8, FontStyle.Bold)
        arrWidth = {90, 80, 75}
        arrFormat = {c.MidLeft, c.MidRight, c.MidRight}

        'nama kolom dipisah dengan ;
        Printer.Print("Item Barang#Qty#Sub Total", arrWidth, arrFormat)
        Printer.SetFont("Courier New", 8, FontStyle.Regular)
        arrWidth = {135, 32, 70}
        arrFormat = {c.MidLeft, c.MidRight, c.MidRight}

        Printer.Print("-----------------------------------")

        'loop item penjualan
        For Each xrow As DataGridViewRow In Me.DataGridView1.Rows
            Printer.Print(Strings.Left(xrow.Cells(0).Value, 15) & "#" & xrow.Cells(1).Value & "#" &
                         (FormatNumber(xrow.Cells(1).Value * xrow.Cells(2).Value, 0)), arrWidth, arrFormat)
            SubTotal += (xrow.Cells(1).Value * xrow.Cells(2).Value)
        Next

        Printer.Print("-----------------------------------")
        arrWidth = {130, 110}
        arrFormat = {c.MidLeft, c.MidRight}

        Printer.Print("Total Bayar #" & FormatNumber(SubTotal, 0), arrWidth, arrFormat)
        Printer.Print("Diskon #" & FormatNumber(0, 0), arrWidth, arrFormat)
        Printer.Print("Diterima #" & FormatNumber(SubTotal, 0), arrWidth, arrFormat)
        Printer.Print("Infaq #" & FormatNumber(0, 0), arrWidth, arrFormat)
        Printer.Print("Kembalian #" & FormatNumber(0, 0), arrWidth, arrFormat)
        Printer.Print("-----------------------------------")

        Footer1 = {250, 0} : Footer2 = {c.MidCenter}
        Printer.SetFont("Courier New", 8, FontStyle.Regular)
        Printer.Print("Terima Kasih", Header1, Header2)
        Printer.Print("Atas Kunjungan Anda", Header1, Header2)

        Printer.DoPrint()

    End Sub

End Class

