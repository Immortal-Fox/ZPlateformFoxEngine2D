Namespace ScreenElements

    ''' <summary>
    ''' Classe de base pour les éléments qui seront affichés sur l'écran de jeu
    ''' Classe Abstraite
    ''' </summary>
    Public MustInherit Class AbstractScreenElement
        ''' <summary>
        ''' Nom de l'objet
        ''' </summary>
        Protected name As String
        ''' <summary>
        ''' Image de l'élément
        ''' </summary>
        Protected img As Bitmap
        ''' <summary>
        ''' Modificateur graphique de l'image
        ''' </summary>
        Protected g As Graphics

        Protected position As Point
        Protected size As Size
        Protected show As Boolean

        ' Propriétés spécifiques à l'héritage

        ' Couleur du rectangle
        Protected rectangleColor As Color


        Sub New()
        End Sub

        ''' <summary>
        ''' Dessine l'image finale de l'élément
        ''' </summary>
        Protected MustOverride Sub Draw()

        ''' <summary>
        ''' Mets à jour la taille de l'élément graphique
        ''' </summary>
        Protected MustOverride Sub UpdateSize()

        ''' <summary>
        ''' Récupère l'image de l'élément
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property GetImage As Bitmap
            Get
                Return img
            End Get
        End Property

        ''' <summary>
        ''' Retourne ou modifie la position de l'élément
        ''' </summary>
        ''' <returns></returns>
        Public Property GetPosition As Point
            Get
                Return position
            End Get
            Set(value As Point)
                position = value
            End Set
        End Property

        ''' <summary>
        ''' Retourne ou modifie la coordonnée X de la position
        ''' </summary>
        ''' <returns></returns>
        Public Property GetX As Integer
            Get
                Return position.X
            End Get
            Set(value As Integer)
                position.X = value
            End Set
        End Property

        ''' <summary>
        ''' Retourne ou modifie la coordonnée Y de la position
        ''' </summary>
        ''' <returns></returns>
        Public Property GetY As Integer
            Get
                Return position.Y
            End Get
            Set(value As Integer)
                position.Y = value
            End Set
        End Property

        ''' <summary>
        ''' Retourne ou modifie la taille du screenElement
        ''' </summary>
        ''' <returns></returns>
        Public Property GetSize As Size
            Get
                Return size
            End Get
            Set(value As Size)
                size = value
            End Set
        End Property

        ''' <summary>
        ''' Affiche ou non l'élément sur l'écran de jeu
        ''' </summary>
        ''' <returns></returns>
        Public Property GetShow As Boolean
            Get
                Return show
            End Get
            Set(value As Boolean)
                show = value
            End Set
        End Property

        ''' <summary>
        ''' Retourne ou modifie le nom du ScreenElement
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
    End Class
End Namespace
