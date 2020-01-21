Public MustInherit Class AbstractGameCore
    ''' <summary>
    ''' GameEngine parent
    ''' </summary>
    Protected parent As GameEngine

    ''' <summary>
    ''' Evènement lors du tick de l'horloge de jeu
    ''' </summary>
    Public MustOverride Sub Tick()

    ''' <summary>
    ''' Evènement lorsqu'une touche calvier est enfoncée
    ''' </summary>
    Public MustOverride Sub KeyDown(sender As Object, e As KeyEventArgs)

    ''' <summary>
    ''' Evènement lorsqu'une touche clavier est relâchée
    ''' </summary>
    Public MustOverride Sub KeyUp(sender As Object, e As KeyEventArgs)

    ''' <summary>
    ''' Evènement lorsque une touche de la souris est enfoncée
    ''' </summary>
    Public MustOverride Sub MouseDown()

    ''' <summary>
    ''' Evènement lorsque la souris se déplace
    ''' </summary>
    Public MustOverride Sub MouseMove()

    ''' <summary>
    ''' Evènement lorsque une touche de la souris est relâchée
    ''' </summary>
    Public MustOverride Sub MouseUp()


End Class

'''' <summary>
'''' Gère la logique de jeu
'''' Ici sera placé le code utilisateur
'''' </summary>
'Public Class GameCoreManager
'    Inherits AbstractGameCore


'    Sub New(ByVal _parent As GameEngine)
'        parent = _parent
'    End Sub

'    Public Overrides Sub Tick()
'        '    Throw New NotImplementedException()
'    End Sub

'    Public Overrides Sub KeyDown(sender As Object, e As KeyEventArgs)
'        '   Throw New NotImplementedException()
'    End Sub

'    Public Overrides Sub KeyUp(sender As Object, e As KeyEventArgs)
'        '   Throw New NotImplementedException()
'    End Sub

'    Public Overrides Sub MouseDown()
'        '   Throw New NotImplementedException()
'    End Sub

'    Public Overrides Sub MouseMove()
'        '    Throw New NotImplementedException()
'    End Sub

'    Public Overrides Sub MouseUp()
'        '    Throw New NotImplementedException()
'    End Sub
'End Class
