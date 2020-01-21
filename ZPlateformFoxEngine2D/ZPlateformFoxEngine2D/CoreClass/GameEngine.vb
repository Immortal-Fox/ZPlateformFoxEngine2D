Imports ZPlateforme.Sprites
Imports ZPlateforme.ScreenElements

''' <summary>
''' Moteur de jeu 2D pour jeu de plateforme en .NET
''' </summary>
Public Class GameEngine

    ' Informations sur le moteur de jeu
    Private ReadOnly author As String = "Ludovic Charmillot @Zyldar @Immortal Fox"
    Private ReadOnly version As String = "Version 1"
    Private ReadOnly name As String = "Fox GameEngine 2D"

    ' Gestion des éléments d'interface du jeu
    Private ReadOnly GameScreenElementsManagement As GameScreenElementsManager
    ' Gestion des sprites du jeu
    Private ReadOnly GameSpritesManagement As GameSpritesManager
    ' Gestion de l'affichage du jeu
    Private ReadOnly GameViewManagement As GameViewManager
    ' Gestion des méthodes de debug
    Private ReadOnly GameDebugManagement As GameDebugManager
    ' Gestion des méthodes d'enregistrement du cache de jeu
    Private ReadOnly GameCacheManagement As GameCacheManager
    ' Gestion des inputs 
    Private ReadOnly GameIOManagement As GameIOManager
    ' Gestion du jeu
    Private ReadOnly GameCoreManagement As GameCoreManager
    ' Gestion des dialogues
    Private ReadOnly GameDialogManagement As GameDialogManager
    ' Gestion des ressources
    Private ReadOnly GameRessourcesManagement As GameRessourcesManager

    ' Cadenceur de jeu
    Private WithEvents GameTick As Timer
    ' Cadenceur de Dialogue
    Private WithEvents GameTickDialog As Timer

    Private sceneSize As Size


    ' Initialise les composants du moteur de jeu
    Sub New()
        sceneSize = New Size(584, 561)
        GameTick = New Timer
        GameTickDialog = New Timer With {
            .Interval = 50
        }

        ' Instancie les classes de gestion de Game Engine
        ' Gestion des ScreenElements
        GameScreenElementsManagement = New GameScreenElementsManager(Me)
        ' Gestion des sprites
        GameSpritesManagement = New GameSpritesManager(Me)
        ' Gestion de l'affichage
        GameViewManagement = New GameViewManager(Me)
        ' Aide au debug
        GameDebugManagement = New GameDebugManager(Me)
        ' Gestion du cache
        GameCacheManagement = New GameCacheManager(Me)
        ' Gestion des IO
        GameIOManagement = New GameIOManager(Me)
        ' Gestion des dialogues de jeu
        GameDialogManagement = New GameDialogManager(Me)
        ' Gestion des ressources
        GameRessourcesManagement = New GameRessourcesManager(Me)
        ' Gestion du jeu
        GameCoreManagement = New GameCoreManager(Me)

        ' Initialise la taille du gameView
        GameViewManagement.InitializeGraphics(New Size(584, 561))
    End Sub

    ''' <summary>
    ''' Arrête le cadenceur de jeu
    ''' </summary>
    Public Sub StopTick()
        GameTick.Stop()
        GameTickDialog.Stop()
    End Sub

    ''' <summary>
    ''' Démarre le cadenceur de jeu
    ''' </summary>
    Public Sub StartTick()
        GameTick.Interval = 18
        GameTick.Start()
        GameTickDialog.Start()
    End Sub

    ''' <summary>
    ''' Définit le contrôle qui servira a afficher le jeu
    ''' </summary>
    ''' <param name="_pboxView"></param>
    Public Sub SetGameView(ByVal _pboxView As PictureBox)
        GameViewManagement.SetViewControl(_pboxView)
    End Sub

    ''' <summary>
    ''' Horloge de jeu
    ''' Cadence le dessin du jeu
    ''' </summary>w
    Protected Sub Tick() Handles GameTick.Tick
        ViewManagement.DrawView()
    End Sub

    ''' <summary>
    ''' Horloge de jeu à la seconde
    ''' </summary>
    Protected Sub TickSecond() Handles GameTickDialog.Tick
        DialogManagement.Tick()
    End Sub

    ' ********************** '
    ' Accès aux champs       '
    ' ********************** '
    Public Property GetSceneSize As Size
        Get
            Return sceneSize
        End Get
        Set(value As Size)
            sceneSize = value
        End Set
    End Property

    ''' <summary>
    ''' Retourne le timer qui rythme la cadence de jeu
    ''' </summary>
    ''' <returns>Timer</returns>
    Public ReadOnly Property GetGameTick As Timer
        Get
            Return GameTick
        End Get
    End Property

    ''' <summary>
    ''' Accès aux méthodes de gestion des ScreenElements
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ScreenElementsManagement As GameScreenElementsManager
        Get
            Return GameScreenElementsManagement
        End Get
    End Property

    ''' <summary>
    ''' Accès aux méthodes de gestion des sprites
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property SpritesManagement As GameSpritesManager
        Get
            Return GameSpritesManagement
        End Get
    End Property

    ''' <summary>
    ''' Accès aux méthodes de gestion de la vue
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ViewManagement As GameViewManager
        Get
            Return GameViewManagement
        End Get
    End Property

    ''' <summary>
    ''' Accès aux méthodes de gestion de debug
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property DebugManagement As GameDebugManager
        Get
            Return GameDebugManagement
        End Get
    End Property

    ''' <summary>
    ''' Accès aux méthodes de gestion du cache
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property CacheManagement As GameCacheManager
        Get
            Return GameCacheManagement
        End Get
    End Property

    ''' <summary>
    ''' Accès aux méthodes de gestion des entrées/sorties
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property IOManagement As GameIOManager
        Get
            Return GameIOManagement
        End Get
    End Property

    ''' <summary>
    ''' Accès à la logique de jeu
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property CoreManagement As GameCoreManager
        Get
            Return GameCoreManagement
        End Get
    End Property

    ''' <summary>
    ''' Accès aux méthodes de gestion des dialogues
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property DialogManagement As GameDialogManager
        Get
            Return GameDialogManagement
        End Get
    End Property

    ''' <summary>
    ''' Accès aux méthodes de gestion des ressources
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property RessourcesManagement As GameRessourcesManager
        Get
            Return GameRessourcesManagement
        End Get
    End Property
End Class
