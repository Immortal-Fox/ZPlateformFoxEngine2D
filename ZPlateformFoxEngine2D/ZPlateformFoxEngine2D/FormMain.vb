''' <summary>
''' Formulaire principal (démarrage)
''' </summary>
Public Class FormMain
    ''' <summary>
    ''' Moteur de jeu
    ''' </summary>
    Public Game As GameEngine

    Private Sub Form_Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' Instancie le moteur de jeu
        Game = New GameEngine
        Game.StartTick()

        ' Passe les objets pour l'utilisation des events
        Game.IOManagement.SetKeyEventSource(Me)
        Game.IOManagement.SetMouseEVentSource(pboxView)

        ' Définit le contrôle pour le dessin du jeu
        Game.SetGameView(pboxView)

        ' Paramètres pour debug
        Game.DebugManagement.SetRawDebugTexte("FPS : {fps}" & vbCrLf & "Sprites : {spritescount}")
        Game.DebugManagement.ShowDebugTexte = True
        Game.DebugManagement.ShowDebugHitbox = False

        ' Cache
        Game.CacheManagement.InitializeCache("C:\Users\charlud\AppData\Roaming\ZRockSystem")
    End Sub
End Class
