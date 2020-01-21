Imports ZPlateforme.Sprites

Namespace Sprites

    ''' <summary>
    ''' Texte flottant
    ''' Avec déplacement
    ''' </summary>
    Public Class FloatingText
        Inherits StaticBackgroundItem

        Protected police As Font
        Protected policeColor As Color
        Protected policeSize As Integer
        Protected text As String
        Protected temporary As Boolean
        Protected aliveTick As Integer

        Sub New()
            police = New Font("Consolas", 12, FontStyle.Bold)
            policeColor = Color.Black
            temporary = True
            text = "FloatingText"
            aliveTick = 60
            ConstructImage()
        End Sub

        Protected Overrides Sub Tick()
            aliveTick = aliveTick - 1
            If aliveTick > 0 Then
                Me.GetY = Me.GetY - 1
                UpdateHitBox()
            Else
                Dispose()
            End If
        End Sub

        ''' <summary>
        ''' Construit l'image contenant le texte
        ''' </summary>
        Protected Sub ConstructImage()

            Dim tempImage As New Bitmap(10, 10)
            Dim g As Graphics = Graphics.FromImage(tempImage)
            Dim textSize As SizeF = g.MeasureString(text, police)
            GetSize = textSize.ToSize
            g.Dispose()

            currentImage = New Bitmap(CInt(textSize.Width), (CInt(textSize.Height)))
            g = Graphics.FromImage(currentImage)
            g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias
            g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
            g.Clear(Color.Transparent)
            g.DrawString(text, police, New SolidBrush(policeColor), New PointF(0, 0))
            g.Dispose()
            tempImage.Dispose()
        End Sub

        ''' <summary>
        ''' Obtient ou modifie la couleur du texte
        ''' </summary>
        ''' <returns></returns>
        Public Property GetTextColor As Color
            Get
                Return policeColor
            End Get
            Set(value As Color)
                policeColor = value
                ConstructImage()

            End Set
        End Property

        ''' <summary>
        ''' Obtient ou modifie le texte
        ''' </summary>
        ''' <returns></returns>
        Public Property GetText As String
            Get
                Return text
            End Get
            Set(value As String)
                text = value
                ConstructImage()

            End Set
        End Property

        ''' <summary>
        ''' Obtient ou modifie la police du texte
        ''' </summary>
        ''' <returns></returns>
        Public Property GetPolice As Font
            Get
                Return police
            End Get
            Set(value As Font)
                police = value
                ConstructImage()
            End Set
        End Property

        ''' <summary>
        ''' Obtient ou modifie la taille de la police du texte
        ''' </summary>
        ''' <returns></returns>
        Public Property GetPoliceSize As Integer
            Get
                Return policeSize
            End Get
            Set(value As Integer)
                policeSize = value
                ConstructImage()
            End Set
        End Property

    End Class
End Namespace
