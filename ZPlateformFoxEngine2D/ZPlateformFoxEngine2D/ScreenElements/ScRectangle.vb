Namespace ScreenElements
    Public Class ScRectangle
        Inherits AbstractScreenElement

        Sub New()
            size = New Size(50, 50)
            UpdateSize()
            Draw()
        End Sub


        Protected Overrides Sub Draw()
            If Not IsNothing(img) Then
                g.Clear(Color.Transparent)
                g.FillRectangle(New SolidBrush(Color.Blue), New Rectangle(0, 0, size.Width, size.Height))
            End If
        End Sub



        Protected Overrides Sub UpdateSize()
            ' Suppression des objets
            If Not IsNothing(g) Then
                g.Dispose()
            End If
            If Not IsNothing(img) Then
                img.Dispose()
            End If

            img = New Bitmap(size.Width, size.Height)
            g = Graphics.FromImage(img)

            ' Dessine le rectangle
            Draw()
        End Sub

    End Class
End Namespace

