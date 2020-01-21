Imports System.IO
''' <summary>
''' Gestion des ressources
''' </summary>
Public Class GameRessourcesManager
    ''' <summary>
    ''' GameEngine parent
    ''' </summary>
    Private ReadOnly parent As GameEngine
    ''' <summary>
    ''' Liste contenant les images
    ''' </summary>
    Private ReadOnly listImages As List(Of RessourceImage)
    ''' <summary>
    ''' Liste contenant les listes de texte
    ''' </summary>
    Private ReadOnly listTextFile As List(Of RessourceTextFile)
    ''' <summary>
    ''' Liste contenant les sons
    ''' </summary>
    Private ReadOnly listSounds As List(Of String)
    ''' <summary>
    ''' Image générée par la classe pour remplacer les images manquantes
    ''' </summary>
    Private noTextureImage As Image

    ''' <summary>
    ''' Créer le gestionnaire de ressource
    ''' </summary>
    ''' <param name="_parent"></param>
    Sub New(ByVal _parent As GameEngine)
        parent = _parent
        listImages = New List(Of RessourceImage)
        listTextFile = New List(Of RessourceTextFile)
        listSounds = New List(Of String)

        ' Créer l'image noTexture

        Dim tempImg As New Bitmap(40, 40)
        Dim tempG As Graphics = Graphics.FromImage(tempImg)
        tempG.DrawString("No Texture", New Font("Consolas", 10), New SolidBrush(Color.Black), New PointF(0, 10))
        tempG.Dispose()
        noTextureImage = tempImg
        tempImg.Dispose()
    End Sub

    Public Function LoadExternalImage(ByVal _path As String) As Image
        Try
            ' Si le fichier existe
            If File.Exists(_path) Then
                Return Image.FromFile(_path)
            Else
                Return noTextureImage
            End If
        Catch

        End Try
        Return Nothing
    End Function

    ''' <summary>
    ''' Ajoute une image avec un nom
    ''' </summary>
    ''' <param name="_image">Image</param>
    ''' <param name="_name">Nom associé à l'image</param>
    Public Sub AddImage(ByVal _image As Image, ByVal _name As String)
        ' Si le nom et l'image de sont pas NULL
        If Not IsNothing(_image) And Not IsNothing(_name) Then
            If Not ImageNameExist(_name) Then
                ' Créer le RessourceImage
                Dim temp As RessourceImage = New RessourceImage With {
                    .image = _image,
                    .name = _name
                }
                ' Et l'ajoute à la liste
                listImages.Add(temp)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Supprime une image
    ''' </summary>
    ''' <param name="_image">Instance de la ressource à supprimer</param>
    Protected Sub RemoveImage(ByVal _image As RessourceImage)
        If listImages.Contains(_image) Then
            listImages.Remove(_image)
        End If
    End Sub

    ''' <summary>
    ''' Supprimer une ressource d'image avec un nom
    ''' </summary>
    ''' <param name="_name">Nom de l'image</param>
    Public Sub RemoveImageByName(ByVal _name As String)
        Dim toRemove As RessourceImage = Nothing
        For Each _image As RessourceImage In listImages
            If _image.name = _name Then
                toRemove = _image
                Exit For
            End If
        Next
        If Not IsNothing(toRemove) Then
            RemoveImage(toRemove)
        End If
    End Sub

    ''' <summary>
    ''' Récupère une image
    ''' </summary>
    ''' <param name="_name">Nom de l'image</param>
    ''' <returns>Image ou rien</returns>
    Public Function GetImage(ByVal _name As String) As Image
        For Each _image As RessourceImage In listImages
            If _image.name = _name Then
                Return _image.image
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' Vérifie si un nom d'image est déjà utilisé
    ''' </summary>
    ''' <param name="_name">Nom de l'image</param>
    ''' <returns>True si le nom existe sinon retourne false</returns>
    Public Function ImageNameExist(ByVal _name As String) As Boolean
        For Each _image As RessourceImage In listImages
            If _image.name = _name Then
                Return True
            End If
        Next
        Return False
    End Function

    ''' <summary>
    ''' Ajouter une ressource "Fichier de texte" depuis un chemin de fichier
    ''' </summary>
    ''' <param name="_path">Chemin vers le fichier</param>
    ''' <param name="_name">Nom de la ressource</param>
    Public Sub AddByReadingTextFile(ByVal _path As String, ByVal _name As String)
        ' Si le nom n'existe pas déja
        If Not TextFileNameExist(_name) Then
            ' Si le fichier existe
            If File.Exists(_path) Then
                Dim temp As New RessourceTextFile With {
                    .name = _name
                }
                ' On va lire chaque ligne et les ajouter dans la liste
                Using sr As New StreamReader(_path)
                    Do While (sr.EndOfStream)
                        temp.text = sr.ReadToEnd()
                    Loop
                End Using
            End If
        End If
    End Sub

    ''' <summary>
    ''' Vérifie si le nom de ressource pour fichier texte existe déjà
    ''' </summary>
    ''' <param name="_name">Nom de ressource</param>
    ''' <returns>True si il existe déjà sinon retourne false</returns>
    Public Function TextFileNameExist(ByVal _name As String) As Boolean
        For Each _textFile As RessourceTextFile In listTextFile
            If _textFile.name = _name Then
                Return True
            End If
        Next
        Return False
    End Function

    ''' <summary>
    ''' Supprime la ressource de texte selon son nom
    ''' </summary>
    ''' <param name="_name">Nom de la ressource</param>
    Public Sub RemoveTextFileByName(ByVal _name As String)
        Dim toRemove As RessourceTextFile = Nothing
        For Each _textFile As RessourceTextFile In listTextFile
            If _textFile.name = _name Then
                toRemove = _textFile
                Exit For
            End If
        Next
        If Not IsNothing(toRemove) Then
            RemoveTextFile(toRemove)
        End If
    End Sub

    ''' <summary>
    ''' Supprime la ressource text
    ''' </summary>
    ''' <param name="_textFile">Instance de la ressource à supprimer</param>
    Private Sub RemoveTextFile(ByVal _textFile As RessourceTextFile)
        If listTextFile.Contains(_textFile) Then
            listTextFile.Remove(_textFile)
        End If
    End Sub

    ''' <summary>
    ''' Récupère l'image par défaut pour les textures inexistantes
    ''' </summary>
    ''' <returns>Image</returns>
    Public ReadOnly Property GetNoTexture As Image
        Get
            Return noTextureImage
        End Get
    End Property

    ''' <summary>
    ''' Structure Image/Nom pour ressource d'image
    ''' </summary>
    Protected Class RessourceImage
        Public name As String
        Public image As Image
    End Class

    ''' <summary>
    ''' Liste de texte/Nom pour ressource de fichier
    ''' </summary>
    Protected Class RessourceTextFile
        Public name As String
        Public text As String
    End Class
End Class
