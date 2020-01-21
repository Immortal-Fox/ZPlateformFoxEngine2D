
''' <summary>
''' Gestionnaire d'inventaire
''' </summary>
Public Class GameInventoryManager
    ''' <summary>
    ''' GameEngine parent
    ''' </summary>
    Protected parent As GameEngine
    ''' <summary>
    ''' Liste des objets
    ''' </summary>
    Protected listObjects As List(Of Short)

    Sub New(ByVal _parent As GameEngine)
        parent = _parent
    End Sub


End Class
