''' <summary>
''' Gestion des Input
''' </summary>
Public Class GameIOManager
    ''' <summary>
    ''' GameEngine parent
    ''' </summary>
    Protected parent As GameEngine
    ''' <summary>
    ''' Source des evènements clavier
    ''' </summary>
    Protected WithEvents KeyEventSource As Form
    ''' <summary>
    ''' Source des evènements souris
    ''' </summary>
    Protected WithEvents MouseEventSource As PictureBox
    ''' <summary>
    ''' Liste des touches enfoncées
    ''' </summary>
    Protected listKeyDown As List(Of Keys)

    ''' <summary>
    ''' Créer un IOManager pour GameEngine
    ''' </summary>
    ''' <param name="_parent"></param>
    Sub New(ByVal _parent As GameEngine)
        listKeyDown = New List(Of Keys)
        parent = _parent
    End Sub
    ''' <summary>
    ''' Définir la source des events clavier
    ''' </summary>
    ''' <param name="_form">Formulaire qui capture les events clavier</param>
    Public Sub SetKeyEventSource(ByVal _form As Form)
        KeyEventSource = _form
    End Sub

    ''' <summary>
    ''' Définir la source des events souris
    ''' </summary>
    ''' <param name="_pictureBox">Controle qui capture les events souris</param>
    Public Sub SetMouseEVentSource(ByVal _pictureBox As PictureBox)
        MouseEventSource = _pictureBox
    End Sub

    ''' <summary>
    ''' Touche enfoncée
    ''' Distribution des events
    ''' </summary>
    Protected Sub KeyDown(sender As Object, e As KeyEventArgs) Handles KeyEventSource.KeyDown
        listKeyDown.Add(e.KeyCode)
        parent.CoreManagement.KeyDown(sender, e)
        parent.DialogManagement.KeyDown(e.KeyCode)
    End Sub

    ''' <summary>
    ''' Touche relachée
    ''' Distribution des events
    ''' </summary>
    Protected Sub KeyUp(sender As Object, e As KeyEventArgs) Handles KeyEventSource.KeyUp
        listKeyDown.Remove(e.KeyCode)
        parent.CoreManagement.KeyUp(sender, e)
    End Sub

    ''' <summary>
    ''' Teste si une touche clavier est enfoncée
    ''' </summary>
    ''' <param name="_key">Code de la touche clavier</param>
    ''' <returns>True si la touche est enfoncée, sinon retourne faux</returns>
    Public Function KeyIsDown(ByVal _key As Keys) As Boolean
        If listKeyDown.Contains(_key) Then
            Return True
        End If
        Return False
    End Function

End Class
