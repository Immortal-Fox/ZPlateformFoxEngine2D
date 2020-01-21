Namespace Sprites
    ''' <summary>
    ''' Plateforme statique
    ''' Collsions On
    ''' </summary>
    Public Class StaticPlateform
        Inherits Sprites.AbstractSprite


        Sub New()
            GetCollisionOn = True
        End Sub


        Protected Overrides Sub Tick()
            '    Throw New NotImplementedException()
        End Sub

        Public Overrides Sub Collide(_sprite As AbstractSprite)

        End Sub
    End Class

End Namespace