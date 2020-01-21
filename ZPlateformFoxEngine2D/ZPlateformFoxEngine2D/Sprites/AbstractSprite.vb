Namespace Sprites

    Enum SPRITETYPE
        VOID
        STATIC_PLATEFORME
        MOVING_PLATEFORME
        PLAYER

    End Enum
    ''' <summary>
    ''' Classe abstraite pour tous les éléments qui seront affichés sur la scène
    ''' </summary>
    Public MustInherit Class AbstractSprite

        ''' <summary>
        ''' Nom du sprite
        ''' Servira pour les scénarios
        ''' </summary>
        Private name As String
        ''' <summary>
        ''' Position du sprite sur la scène
        ''' </summary>
        Private position As Point
        ''' <summary>
        ''' Taille du sprite
        ''' </summary>
        Private size As Size
        ''' <summary>
        ''' Hitbox du sprite
        ''' </summary>
        Private hitbox As Rectangle
        ''' <summary>
        ''' Liste d'images pour affichage
        ''' </summary>
        Public listImage As New List(Of Image)
        ''' <summary>
        ''' Image courante qui sera dessinée
        ''' </summary>
        Public currentImage As Image
        ''' <summary>
        ''' Index de l'image courante
        ''' </summary>
        Public currentImageIndex As Integer
        ''' <summary>
        ''' Horloge de jeu
        ''' </summary>
        Private WithEvents TickHandler As Timer
        ''' <summary>
        ''' Accès aux méthodes de GameEngine
        ''' </summary>
        Public parentGameEngine As GameEngine


        ' Propriétés des sprites
        Private visible As Boolean        ' Le sprite est visible à l'écran
        Private collisionOn As Boolean    ' Les collisions du sprite sont activent

        Public Event OnDestroyed()

#Region "Méthodes"

        ''' <summary>
        ''' Retourne l'image actuellement affichée par le sprite
        ''' </summary>
        ''' <returns>Image du sprite</returns>
        Public Function GetCurrentImage() As Image
            If Not IsNothing(currentImage) Then
                Return currentImage
            End If
            Return Nothing
        End Function

        ''' <summary>
        ''' Change l'image courante
        ''' L'image courante est l'image affichée sur la scène
        ''' </summary>
        ''' <param name="_index"></param>
        Public Sub SetCurrentImage(ByVal _index As Integer)
            If Not IsNothing(listImage) Then
                If listImage.Count > _index Then
                    currentImage = listImage(_index)
                    currentImageIndex = _index
                End If
            End If
        End Sub

        ''' <summary>
        ''' Ajoute une image dans la liste des images
        ''' </summary>
        ''' <param name="_image"></param>
        Public Sub AddImage(ByVal _image As Image)
            If Not IsNothing(_image) Then
                If Not IsNothing(listImage) Then
                    listImage.Add(_image)
                    currentImage = _image
                    currentImageIndex = listImage.Count - 1
                End If
            End If
        End Sub

        ''' <summary>
        ''' Horloge qui définit la cadence du jeu
        ''' </summary>
        ''' <param name="_timer"></param>
        Public Sub SetTickHandler(ByVal _timer As Timer)
            TickHandler = _timer
            ' Ajoute l'évènement tick
            'AddHandler TickHandler.Tick, AddressOf Tick
        End Sub

        ''' <summary>
        ''' Met à jour la hitbox du sprite
        ''' </summary>
        Public Sub UpdateHitBox()
            hitbox.Location = position
            hitbox.Size = size
        End Sub

        ''' <summary>
        ''' Tick de l'horloge de jeu
        ''' La méthode doit être override
        ''' </summary>
        Protected MustOverride Sub Tick() Handles TickHandler.Tick

        ''' <summary>
        ''' Méthode qui doit être appellée en cas de collision avec un objet
        ''' La méthode doit être override
        ''' </summary>
        ''' <param name="_sprite">AbstractSprite</param>
        Public MustOverride Sub Collide(ByVal _sprite As AbstractSprite)


        ''' <summary>
        ''' Suppression de l'objet
        ''' </summary>
        Public Sub Dispose()
            FormMain.Game.SpritesManagement.RemoveSprite(Me)
            Me.TickHandler = Nothing
            For i As Integer = 0 To listImage.Count - 1
                listImage.First.Dispose()
            Next
            currentImage.Dispose()
        End Sub
#End Region

#Region "Accès aux champs"
        ''' <summary>
        ''' Retourne la hitbox du sprite
        ''' </summary>
        ''' <returns>Hitbox</returns>
        Public ReadOnly Property GetHitbox As Rectangle
            Get
                Return hitbox
            End Get
        End Property

        ''' <summary>
        ''' Retourne la position du sprite
        ''' </summary>
        ''' <returns>Position du sprite</returns>
        Public Property GetPosition As Point
            Get
                Return position
            End Get
            Set(value As Point)
                position = value
                UpdateHitBox()
            End Set
        End Property

        ''' <summary>
        ''' Retourne la coordonnée X de la position du sprite
        ''' </summary>
        ''' <returns>Position.X</returns>
        Public Property GetX As Integer
            Get
                Return position.X
            End Get
            Set(value As Integer)
                position.X = value
                UpdateHitBox()
            End Set
        End Property

        ''' <summary>
        ''' Retourne la coordonnée Y de la position du sprite
        ''' </summary>
        ''' <returns>Position.Y</returns>
        Public Property GetY As Integer
            Get
                Return position.Y
            End Get
            Set(value As Integer)
                position.Y = value
            End Set
        End Property

        ''' <summary>
        ''' Retourne la taille du sprite
        ''' </summary>
        ''' <returns>Taille</returns>
        Public Property GetSize As Size
            Get
                Return size
            End Get
            Set(value As Size)
                size = value
                UpdateHitBox()
            End Set
        End Property

        ''' <summary>
        ''' Récupère la coordonnée Y du haut
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Top As Integer
            Get
                Return Me.position.Y
            End Get
        End Property

        ''' <summary>
        ''' Récupère la coordonnée Y du bas
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Bottom As Integer
            Get
                Return (Me.position.Y + Me.size.Height)
            End Get
        End Property

        ''' <summary>
        ''' Récupère la coordonnée X de gauche
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Left As Integer
            Get
                Return Me.position.X
            End Get
        End Property

        ''' <summary>
        ''' Récupère la coordonnée X de la droite
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Right As Integer
            Get
                Return (Me.position.X + Me.size.Width)
            End Get
        End Property

        ''' <summary>
        ''' Retourne l'état des collsions du sprite
        ''' </summary>
        ''' <returns>Etat des collsions</returns>
        Public Property GetCollisionOn As Boolean
            Get
                Return collisionOn
            End Get
            Set(value As Boolean)
                collisionOn = value
            End Set
        End Property

        ''' <summary>
        ''' Retourne ou modifie le nom du sprite
        ''' </summary>
        ''' <returns></returns>
        Public Property GetName As String
            Get
                Return name
            End Get
            Set(value As String)
                name = value
            End Set
        End Property

        ''' <summary>
        ''' Retourne ou modifie le parent du Sprite
        ''' </summary>
        ''' <returns></returns>
        Public Property GetParent As GameEngine
            Get
                Return parentGameEngine
            End Get
            Set(value As GameEngine)
                parentGameEngine = value
            End Set
        End Property

        ''' <summary>
        ''' Retourne ou modifie la valeur indiquant si l'objet est invisible
        ''' </summary>
        ''' <returns></returns>
        Public Property GetVisible As Boolean
            Get
                Return visible
            End Get
            Set(value As Boolean)
                visible = value
            End Set
        End Property
#End Region

    End Class
End Namespace