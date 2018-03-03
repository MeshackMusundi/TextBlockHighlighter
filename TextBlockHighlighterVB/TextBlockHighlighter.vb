Public Class TextBlockHighlighter

    Public Shared Function GetSelection(ByVal element As DependencyObject) As String
        If element Is Nothing Then Throw New ArgumentNullException("element")
        Return element.GetValue(SelectionProperty)
    End Function

    Public Shared Sub SetSelection(ByVal element As DependencyObject, ByVal value As String)
        If element Is Nothing Then Throw New ArgumentNullException("element")
        element.SetValue(SelectionProperty, value)
    End Sub

    Public Shared ReadOnly SelectionProperty As DependencyProperty =
        DependencyProperty.RegisterAttached("Selection", GetType(String), GetType(TextBlockHighlighter),
                                            New PropertyMetadata(New PropertyChangedCallback(AddressOf SelectText)))

    Private Shared Sub SelectText(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
        If d Is Nothing Then Exit Sub
        If TypeOf d IsNot TextBlock Then Throw New InvalidOperationException("Only valid for TextBlock")

        Dim txtBlock As TextBlock = CType(d, TextBlock)
        Dim text As String = txtBlock.Text
        If String.IsNullOrEmpty(text) Then Exit Sub

        Dim highlightText As String = CStr(d.GetValue(SelectionProperty))
        If String.IsNullOrEmpty(highlightText) Then Exit Sub

        Dim index = text.IndexOf(highlightText, StringComparison.CurrentCultureIgnoreCase)
        If index < 0 Then Exit Sub

        Dim selectionColor As Brush = CType(d.GetValue(HighlightColorProperty), Brush)
        Dim forecolor As Brush = CType(d.GetValue(ForecolorProperty), Brush)

        txtBlock.Inlines.Clear()
        While True
            txtBlock.Inlines.AddRange(New Inline() {
                                      New Run(text.Substring(0, index)),
                                      New Run(text.Substring(index, highlightText.Length)) _
                                      With {.Background = selectionColor, .Foreground = forecolor}
                                      })

            text = text.Substring(index + highlightText.Length)
            index = text.IndexOf(highlightText, StringComparison.CurrentCultureIgnoreCase)

            If index < 0 Then
                txtBlock.Inlines.Add(New Run(text))
                Exit While
            End If
        End While
    End Sub


    Public Shared Function GetHighlightColor(ByVal element As DependencyObject) As Brush
        If element Is Nothing Then Throw New ArgumentNullException("element")
        Return element.GetValue(HighlightColorProperty)
    End Function

    Public Shared Sub SetHighlightColor(ByVal element As DependencyObject, ByVal value As Brush)
        If element Is Nothing Then Throw New ArgumentNullException("element")
        element.SetValue(HighlightColorProperty, value)
    End Sub

    Public Shared ReadOnly HighlightColorProperty As DependencyProperty =
        DependencyProperty.RegisterAttached("HighlightColor", GetType(Brush), GetType(TextBlockHighlighter),
                                            New PropertyMetadata(Brushes.Yellow,
                                                                 New PropertyChangedCallback(AddressOf SelectText)))


    Public Shared Function GetForecolor(ByVal element As DependencyObject) As Brush
        If element Is Nothing Then Throw New ArgumentNullException("element")
        Return element.GetValue(ForecolorProperty)
    End Function

    Public Shared Sub SetForecolor(ByVal element As DependencyObject, ByVal value As Brush)
        If element Is Nothing Then Throw New ArgumentNullException("element")
        element.SetValue(ForecolorProperty, value)
    End Sub

    Public Shared ReadOnly ForecolorProperty As DependencyProperty =
        DependencyProperty.RegisterAttached("Forecolor", GetType(Brush), GetType(TextBlockHighlighter),
                                            New PropertyMetadata(Brushes.Black,
                                                                 New PropertyChangedCallback(AddressOf SelectText)))

End Class
