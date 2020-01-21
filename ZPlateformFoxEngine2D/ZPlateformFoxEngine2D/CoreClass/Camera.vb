
''' <summary>
''' Représente une caméra pour la scène
''' </summary>
Public Class Camera

    ' Nom de la camera
    Private name As String
    ' Point de dessin de la camera
    Private positionView As Point
    ' Rectangle de capture de la camera
    Private rectCamera As Rectangle
    ' Défini si la caméra est active ou pas
    Private enabled As Boolean

    ''' <summary>
    ''' Créer une nouvelle camera
    ''' </summary>
    Sub New()

        rectCamera = New Rectangle
        positionView = New Point(0, 0)

    End Sub

    ''' <summary>
    ''' Propriété qui active ou non le dessin de l'image de la camera
    ''' </summary>
    ''' <returns></returns>
    Public Property GetEnabled As Boolean
        Get
            Return enabled
        End Get
        Set(value As Boolean)
            enabled = value
        End Set
    End Property

    ''' <summary>
    ''' Retourne ou modifie le nom de la camera
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
    ''' Retourne ou modifie la position de capture de la camera
    ''' </summary>
    ''' <returns></returns>
    Public Property GetPosition As Point
        Get
            Return rectCamera.Location
        End Get
        Set(value As Point)
            rectCamera.Location = value
        End Set
    End Property

    ''' <summary>
    ''' Retourne ou modifie la position de dessin de la camera
    ''' </summary>
    ''' <returns></returns>
    Public Property GetPositionView As Point
        Get
            Return positionView
        End Get
        Set(value As Point)
            positionView = value
        End Set
    End Property

    ''' <summary>
    ''' Retourne ou modifie la taille de capture de la camera
    ''' </summary>
    ''' <returns></returns>
    Public Property GetSize As Size
        Get
            Return rectCamera.Size
        End Get
        Set(value As Size)
            rectCamera.Size = value
        End Set
    End Property

    ''' <summary>
    ''' Retourne ou modifie le rectangle de capture de la camera
    ''' </summary>
    ''' <returns></returns>
    Public Property GetRectangle As Rectangle
        Get
            Return rectCamera
        End Get
        Set(value As Rectangle)
            rectCamera = value
        End Set
    End Property
End Class
