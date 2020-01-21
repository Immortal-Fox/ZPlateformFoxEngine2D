Imports ZPlateform.ScreenElements

''' <summary>
''' Classe qui gère les ScreenElements
''' Il s'agit de l'interface visuelle du jeu
''' </summary>
Public Class GameScreenElementsManager
    ''' Accès à GameEngine
    Protected parent As GameEngine
    ''' <summary>
    ''' Liste des ScreenElements
    ''' </summary>
    Protected listScreenElements As List(Of AbstractScreenElement)

    ''' <summary>
    ''' Créer la classe de gestion des ScreenElements
    ''' </summary>
    ''' <param name="_parent"></param>
    Sub New(ByVal _parent As GameEngine)
        parent = _parent
        listScreenElements = New List(Of AbstractScreenElement)
    End Sub

    ''' <summary>
    ''' Retourne le premier ScreenElement sur un point
    ''' </summary>
    ''' <param name="_point">Point visé</param>
    ''' <returns>ScreenElement se trouvant sur ce point</returns>
    Public Function GetScreenElementAtPoint(ByVal _point As Point) As AbstractScreenElement
        For Each _screenElement As AbstractScreenElement In listScreenElements
            If _point.X > _screenElement.GetX And _point.X < _screenElement.GetX + _screenElement.GetSize.Width Then
                If _point.Y > _screenElement.GetY And _point.Y < _screenElement.GetY + _screenElement.GetSize.Height Then
                    Return _screenElement
                End If
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' Ajouter un élément d'interface
    ''' </summary>
    ''' <param name="_screenElement"></param>
    Public Sub AddScreenElement(ByVal _screenElement As AbstractScreenElement)
        If Not IsNothing(_screenElement) Then
            listScreenElements.Add(_screenElement)
        End If
    End Sub

    ''' <summary>
    ''' Retourne le ScreenElements selon son nom
    ''' </summary>
    ''' <param name="_name">Nom du ScreenElements voulu</param>
    ''' <returns>ScreenElement</returns>
    Public Function GetScreenElementByName(ByVal _name As String) As AbstractScreenElement
        If Not IsNothing(_name) Then
            For Each _screenElements As AbstractScreenElement In listScreenElements
                If _screenElements.GetName = _name Then
                    Return _screenElements
                End If
            Next
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' Supprime un ScreenElement
    ''' </summary>
    ''' <param name="_screenElement">Instance de l'objet à supprimer</param>
    Public Sub RemoveScreenElement(ByVal _screenElement As AbstractScreenElement)
        If Not IsNothing(_screenElement) Then
            If listScreenElements.Contains(_screenElement) Then
                listScreenElements.Remove(_screenElement)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Supprime un ScreenElement selon son nom
    ''' </summary>
    ''' <param name="_name">Nom du ScreenElement</param>
    Public Sub RemoveScreenElementByName(ByVal _name As String)
        Dim toRemove As AbstractScreenElement = Nothing
        If Not IsNothing(_name) Then
            For Each _screenElement As AbstractScreenElement In listScreenElements
                If _screenElement.GetName = _name Then
                    toRemove = _screenElement
                    Exit For
                End If
            Next
        End If
        listScreenElements.Remove(toRemove)
    End Sub

    ''' <summary>
    ''' Permet de tester l'existence d'un nom de ScreenElement
    ''' </summary>
    ''' <param name="_name">Nom du screen Element</param>
    ''' <returns>Retourne true si le nom existe sinon retourne false</returns>
    Public Function NameExist(ByVal _name As String) As Boolean
        For Each _screenElement As AbstractScreenElement In listScreenElements
            If _screenElement.GetName = _name Then
                Return True
            End If
        Next
        Return False
    End Function

    ''' <summary>
    ''' Retourne la liste des ScreenElements
    ''' </summary>
    ''' <returns>Liste des ScreenElements</returns>
    Public ReadOnly Property GetListScreenElements As List(Of AbstractScreenElement)
        Get
            Return listScreenElements
        End Get
    End Property

End Class
