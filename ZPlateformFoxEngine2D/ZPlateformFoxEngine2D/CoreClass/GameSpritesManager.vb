Imports ZPlateform.Sprites

''' <summary>
''' Classe qui gère les Sprites
''' Il s'agit des objets affichés sur l'écran de jeu
''' </summary>
Public Class GameSpritesManager

    ''' <summary>
    ''' GameEngine parent
    ''' </summary>
    Private parent As GameEngine
    ''' <summary>
    ''' Liste contenant les sprites
    ''' </summary>
    Private listSprites As List(Of AbstractSprite)

    ''' <summary>
    ''' Créer le SpriteManager
    ''' </summary>
    ''' <param name="_parent"></param>
    Sub New(ByVal _parent As GameEngine)
        parent = _parent
        listSprites = New List(Of AbstractSprite)
    End Sub

    ''' <summary>
    ''' Ajoute un sprite à la scène
    ''' Lui ajoute l'horloge de jeu et un accès à GameEngine
    ''' </summary>
    ''' <param name="_sprite"></param>
    Public Sub AddSprite(ByVal _sprite As AbstractSprite, Optional _withTickHandler As Boolean = True)
        _sprite.SetTickHandler(parent.GetGameTick)
        _sprite.GetParent = parent
        listSprites.Add(_sprite)
    End Sub

    ''' <summary>
    ''' Retourne le premier sprite sur un point
    ''' </summary>
    ''' <param name="_point">Point visé</param>
    ''' <returns>Sprite se trouvant sur ce point</returns>
    Public Function GetSpriteAtPoint(ByVal _point As Point) As AbstractSprite
        For Each _sprite As AbstractSprite In listSprites
            If _point.X > _sprite.GetX And _point.X < _sprite.GetX + _sprite.GetSize.Width Then
                If _point.Y > _sprite.GetY And _point.Y < _sprite.GetY + _sprite.GetSize.Height Then
                    Return _sprite
                End If
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' Retourne un sprite selon son nom
    ''' </summary>
    ''' <param name="_name">Nom du sprite</param>
    ''' <returns>Sprite</returns>
    Public Function GetSpriteByName(ByVal _name As String) As AbstractSprite
        If listSprites.Count > 0 Then
            For Each _sprite As AbstractSprite In listSprites
                If _sprite.GetName = _name Then
                    Return _sprite
                End If
            Next
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' Permet de récupérer la liste des sprites qui sont visible sur la scène
    ''' Va servir pour le dessin des éléments
    ''' </summary>
    ''' <param name="_rectScene">Rectangle de la scene</param>
    ''' <returns>Liste des sprites visibles sur la scène</returns>
    Public Function GetInSceneSprites(ByVal _rectScene As Rectangle) As List(Of AbstractSprite)
        Dim listInViewSprites As New List(Of AbstractSprite)
        For Each _spriteElement As AbstractSprite In listSprites
            If _spriteElement.GetHitbox.IntersectsWith(_rectScene) Then
                listInViewSprites.Add(_spriteElement)
            End If
        Next
        Return listInViewSprites
    End Function

    ''' <summary>
    ''' Permet de récupérer la liste des sprites qui collisionne avec le sprite en paramètre
    ''' </summary>
    ''' <param name="_sprite">Sprite</param>
    ''' <returns>Liste des sprite en collision avec Sprite</returns>
    Public Function GetCollidingSprites(ByVal _sprite As AbstractSprite) As List(Of AbstractSprite)

        Dim listCollidingSprites As New List(Of AbstractSprite)
        For Each _spriteElement As AbstractSprite In listSprites
            If _spriteElement.GetCollisionOn And _spriteElement.Equals(_sprite) = False Then
                If _spriteElement.GetHitbox.IntersectsWith(_sprite.GetHitbox) Then
                    listCollidingSprites.Add(_spriteElement)
                End If
            End If
        Next
        Return listCollidingSprites
    End Function

    ''' <summary>
    ''' Retrourne tous les sprites
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllSprites() As List(Of AbstractSprite)
        Return listSprites
    End Function

    ''' <summary>
    ''' Supprime un sprite
    ''' </summary>
    ''' <param name="_sprite">Instance du sprite à supprimer</param>
    Public Sub RemoveSprite(ByVal _sprite As AbstractSprite)
        If listSprites.Contains(_sprite) Then
            listSprites.Remove(_sprite)
        End If
    End Sub

    ''' <summary>
    ''' Supprime tous les sprites de la liste
    ''' </summary>
    Public Sub RemoveAllSprites()
        listSprites.Clear()
    End Sub

    ''' <summary>
    ''' Vérifie l'existence d'un nom de sprite
    ''' </summary>
    ''' <param name="_name">Nom du sprite</param>
    ''' <returns>Retourne true si le nom de sprite existe sinon retourne false</returns>
    Public Function NameExist(ByVal _name As String) As Boolean
        For Each _sprites As AbstractSprite In listSprites
            If _sprites.GetName = _name Then
                Return True
            End If
        Next
        Return False
    End Function

    ''' <summary>
    ''' Retourne la liste des sprites
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property GetListSprites As List(Of AbstractSprite)
        Get
            Return listSprites
        End Get
    End Property
End Class
