''' <summary>
''' Fournit des méthodes pour aider au deboggage 
''' </summary>
Public Class GameDebugManager
    ''' <summary>
    ''' Accès aux méthodes du parent
    ''' </summary>
    Private parent As GameEngine
    ''' <summary>
    ''' Indique si le mode de debug est actif
    ''' </summary>

    Private debugText As Boolean
    Private debugHitbox As Boolean

    Private rawTextDebug As String

    ''' <summary>
    ''' Liste contenant les logs
    ''' </summary>
    Private ReadOnly listLogs As List(Of String)


    Sub New(ByVal _parent As GameEngine)
        listLogs = New List(Of String)
        parent = _parent
    End Sub

    ''' <summary>
    ''' Ajoute une ligne dans texte dans les logs
    ''' </summary>
    ''' <param name="_text"></param>
    Public Sub Log(ByVal _text As String)
        listLogs.Add(_text)
    End Sub

    ''' <summary>
    ''' Définit le texte brut contenant les nom de variable entre accolade
    ''' </summary>
    ''' <param name="_rawText"></param>
    Public Sub SetRawDebugTexte(ByVal _rawText As String)
        rawTextDebug = _rawText
    End Sub

    ''' <summary>
    ''' Retourne le texte avec la conversion des noms de variable en valeur
    ''' </summary>
    ''' <returns>Texte de debug</returns>
    Public Function GetTranslatedDebugText() As String
        Dim s As String = rawTextDebug
        s = s.Replace("{spritescount}", CStr(parent.SpritesManagement.GetListSprites.Count))
        s = s.Replace("{screenelementscount}", CStr(parent.ScreenElementsManagement.GetListScreenElements.Count))
        s = s.Replace("{fps}", CStr(parent.ViewManagement.GetDebugFPS))
        s = s.Replace("{gametick}", parent.GetGameTick.Interval & "ms")
        s = s.Replace("{dialogcount}", CStr(parent.DialogManagement.DebugDialogCount))
        Return s
    End Function

    ''' <summary>
    ''' Définit ou retourne le GameEngine parent
    ''' </summary>
    ''' <returns></returns>
    Public Property GetParent As GameEngine
        Get
            Return parent
        End Get
        Set(value As GameEngine)
            parent = value
        End Set
    End Property

    ''' <summary>
    ''' Affiche ou non le texte de debug définit dans rawdebugtext
    ''' </summary>
    ''' <returns></returns>
    Public Property ShowDebugTexte As Boolean
        Get
            Return debugText
        End Get
        Set(value As Boolean)
            DebugText = value
        End Set
    End Property

    ''' <summary>
    ''' Affiche ou non le rectangle des hitbox dans gameview
    ''' </summary>
    ''' <returns></returns>
    Public Property ShowDebugHitbox As Boolean
        Get
            Return debugHitbox
        End Get
        Set(value As Boolean)
            debugHitbox = value
        End Set
    End Property
End Class
