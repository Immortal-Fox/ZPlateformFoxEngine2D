Imports System.IO

''' <summary>
''' Gère la logique de jeu
''' Ici sera placé le code utilisateur
''' </summary>
Public Class GameCoreManager
    Inherits AbstractGameCore

    Protected GamePlayer1 As Sprites.Player

    Sub New(ByVal _parent As GameEngine)
        parent = _parent
        Initialize()
    End Sub

    Public Sub Initialize()
        ' Démarre le game tick
        parent.StartTick()

        ' charge des images pour tester
        For Each _fichier As String In Directory.GetFiles("C:\Users\ludovic\AppData\Roaming\ZRockSystem\Images")
            parent.RessourcesManagement.AddImage(Image.FromFile(_fichier), Path.GetFileName(_fichier))
        Next


        GamePlayer1 = New Sprites.Player With {
            .GetPosition = New Point(20, -120),
            .GetSize = New Size(26, 40)
        }

        GamePlayer1.AddImage(parent.RessourcesManagement.GetImage("terre.png"))

        'GamePlayer1.AddImage(Image.FromFile("C:\Users\charlud\Desktop\joueur_arret.png"))
        parent.SpritesManagement.AddSprite(GamePlayer1)

        Randomize()
        ' dev test
        Dim bmp As Image = Image.FromFile("C:\Users\ludovic\AppData\Roaming\ZRockSystem\Images\terre.png")
        ' Dim g As Graphics = Graphics.FromImage(bmp)
        ' g.Clear(Color.Red)
        Dim nb As Integer
        For x As Integer = 0 To 20
            For y As Integer = 10 To -60 Step -1
                Dim n As Integer = CInt(Rnd() * 20)
                If n = 0 Or n = 1 Or n = 2 Then
                    Dim f = CreateMovingPlateform(New Point(40 * x, y * 40), New Size(40, 10), bmp)
                    parent.SpritesManagement.AddSprite(f)
                ElseIf n = 3 Then
                    Dim s As New Sprites.Money With {
                        .GetPosition = New Point(40 * x, 40 * y),
                        .GetSize = New Size(40, 40),
                        .GetMoneyValue = CInt(Rnd() * 10)
                    }
                    parent.SpritesManagement.AddSprite(s)
                End If

                nb += 1

            Next
        Next
        parent.SpritesManagement.AddSprite(CreateStaticPlateform(New Point(0, 100), New Size(200, 20), bmp))

        ' Dialogue de test
        parent.DialogManagement.CreateDialog("Test")
        Dim mess As New GameMessageDialog With {
            .text = "Yolo un deux trois 4 5 six etc",
            .title = "Un teknar",
            .image = parent.RessourcesManagement.GetImage("terre.png")
        }
        parent.DialogManagement.AddMessageToLastCreatedDialog(mess)
        mess = New GameMessageDialog With {
            .text = "Un autre message pour jean claude",
            .title = "Un teknar",
            .skipAutoAfter = False,
            .textBackgroundColor = Color.SandyBrown
        }
        parent.DialogManagement.AddMessageToLastCreatedDialog(mess)
        parent.DialogManagement.ShowDialog("Test")

    End Sub

    Public Overrides Sub Tick()
        '    Throw New NotImplementedException()
    End Sub

    Public Overrides Sub KeyDown(sender As Object, e As KeyEventArgs)
        Select Case e.KeyCode
            Case Keys.A
                GamePlayer1.KeysDown(1) ' Right
            Case Keys.D
                GamePlayer1.KeysDown(2) ' Left
            Case Keys.S
                GamePlayer1.KeysDown(3) ' Down
            Case Keys.W
                GamePlayer1.KeysDown(4) ' Up
        End Select
    End Sub

    Public Overrides Sub KeyUp(sender As Object, e As KeyEventArgs)
        Select Case e.KeyCode
            Case Keys.A
                GamePlayer1.KeysUp(1) ' Right
            Case Keys.D
                GamePlayer1.KeysUp(2) ' Left
            Case Keys.S
                GamePlayer1.KeysUp(3) ' Down
            Case Keys.W
                GamePlayer1.KeysUp(4) ' Up
        End Select
    End Sub

    Public Overrides Sub MouseDown()
        '   Throw New NotImplementedException()
    End Sub

    Public Overrides Sub MouseMove()
        '    Throw New NotImplementedException()
    End Sub

    Public Overrides Sub MouseUp()
        '    Throw New NotImplementedException()
    End Sub
End Class
