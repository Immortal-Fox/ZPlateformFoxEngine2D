
''' <summary>
''' Gestion du cache de jeu
''' Ajouter, Lire des données
''' </summary>
Public Class GameCacheManager
    ''' <summary>
    ''' Game Engine parent
    ''' </summary>
    Private ReadOnly parent As GameEngine
    ''' <summary>
    ''' Objet pour lire le fichier de cache
    ''' </summary>
    Private pFile As ParameterFileReader
    ''' <summary>
    ''' Chemin vers le cache du jeu
    ''' </summary>
    Private cachePath As String


    ''' <summary>
    ''' Ce déclenche lors que le cache a été écrit
    ''' </summary>
    Event CacheSaved()
    Event CacheReaded()

    Sub New(ByVal _parent As GameEngine)
        parent = _parent
    End Sub

    ''' <summary>
    ''' Initialise le système de cache
    ''' </summary>
    ''' <param name="_cachePath">Chemin d'accès au fichier de cache</param>
    Public Sub InitializeCache(Optional ByVal _cachePath As String = "")
        If _cachePath = "" Then
            If cachePath = "" Then
                ' Erreur, aucun chemin d'accès n'est définit
                Exit Sub
            Else
                _cachePath = cachePath
            End If
        End If
        ' Créer le lecteur de fichier de paramètres
        pFile = New ParameterFileReader()
        ' Va lire le fichier de cache à l'endroit indiqué
        pFile.ReadFile(_cachePath)
        RaiseEvent CacheReaded()
    End Sub

    ''' <summary>
    ''' Enregistre le fichier de cache
    ''' </summary>
    Public Sub WriteCache()
        pFile.WriteFile(cachePath)
        RaiseEvent CacheSaved()
    End Sub

    ''' <summary>
    ''' Ajoute une valeur associée à un nom dans le cache
    ''' </summary>
    ''' <param name="_name">Nom du cache</param>
    ''' <param name="_value">Valeur du cache</param>
    Public Sub AddCacheValue(ByVal _name As String, ByVal _value As String)
        pFile.AddParameter(_name, _value)
    End Sub

    ''' <summary>
    ''' Modifie la valeur d'un paramètre de cache
    ''' </summary>
    ''' <param name="_name">Nom du cache</param>
    ''' <param name="_value"></param>
    Public Sub SetCacheValue(ByVal _name As String, ByVal _value As String)
        pFile.SetParameter(_name, _value)
    End Sub

    ''' <summary>
    ''' Récupère la valeur du cache
    ''' </summary>
    ''' <param name="_name">Nom du cache</param>
    ''' <returns>Valeur du cache</returns>
    Public Function GetCacheValue(ByVal _name As String) As String
        Return pFile.GetParameter(_name)
    End Function

    ''' <summary>
    ''' Retourne ou modifie le chemin d'accès au fichier du cache de jeu
    ''' </summary>
    ''' <returns></returns>
    Public Property GetCachePath As String
        Get
            Return cachePath
        End Get
        Set(value As String)
            cachePath = value
        End Set
    End Property
End Class
