Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.OleDb.OleDbConnection
Imports System.Data.OleDb
Imports Word = Microsoft.Office.Interop.Word


Public Class Form1

    

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim documentFormat As Object = 8
        Dim DirInfo As IO.DirectoryInfo
        Dim WithoutExe, outputFilename As String
        DirInfo = New IO.DirectoryInfo(TextBox3.Text)
        For Each file In DirInfo.GetFiles("*.doc", IO.SearchOption.TopDirectoryOnly) 'Find only for docx file



            WithoutExe = Path.GetFileNameWithoutExtension(file.Name)
            
            Dim objApp As New Word.Application
            objApp.Visible = True

            'Open an existing document.  
            Dim objDoc As Word.Document = objApp.Documents.Open(TextBox3.Text & "\" & file.Name)
            objDoc = objApp.ActiveDocument

            'Find and replace some text  
            objDoc.Content.Find.Execute(FindText:=TextBox1.Text, ReplaceWith:=TextBox2.Text, Replace:=Word.WdReplace.wdReplaceAll)
            While objDoc.Content.Find.Execute(FindText:="  ", Wrap:=Word.WdFindWrap.wdFindContinue)
                objDoc.Content.Find.Execute(FindText:="  ", ReplaceWith:=" ", Replace:=Word.WdReplace.wdReplaceAll, Wrap:=Word.WdFindWrap.wdFindContinue)
            End While

            'outputFilename = System.IO.Path.ChangeExtension(file.Name, "htm")
            ''Close
            objDoc.SaveAs(TextBox3.Text & "\" & WithoutExe, documentFormat)
            
            objDoc.Close()
            objApp.Quit()
            objDoc = Nothing
            objApp = Nothing

        Next file



    End Sub

   
End Class
