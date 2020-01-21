Namespace Sprites
    Public Class Money
        Inherits AbstractSprite
        Protected moneyValue As Integer



        Sub New()
            GetCollisionOn = True
            ' AddImage(FormMain.Game.RessourcesManagement.LoadExternalImage("C:\Users\ludovic\Desktop\Money.png"))
            AddImage(Image.FromFile("C:\Users\ludovic\Desktop\Money.png"))
        End Sub

        Protected Overrides Sub Tick()

        End Sub

        Public Overrides Sub Collide(_sprite As AbstractSprite)
            Dispose()
            Dim s As New FloatingText With {
                .GetText = "+" & moneyValue,
                .GetPosition = Me.GetPosition,
                .GetTextColor = Color.Gold,
                .GetPoliceSize = 12
            }
            FormMain.Game.SpritesManagement.AddSprite(s)

        End Sub

        ''' <summary>
        ''' Retourne ou modifie la valeur de la monnaie
        ''' </summary>
        ''' <returns></returns>
        Public Property GetMoneyValue As Integer
            Get
                Return moneyValue
            End Get
            Set(value As Integer)
                moneyValue = value
            End Set
        End Property

    End Class
End Namespace

