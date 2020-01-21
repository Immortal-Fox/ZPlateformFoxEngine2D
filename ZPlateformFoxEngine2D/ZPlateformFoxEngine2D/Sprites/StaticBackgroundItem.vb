
Namespace Sprites
    Public Class StaticBackgroundItem
        Inherits AbstractSprite

        Sub New()
            GetCollisionOn = False
        End Sub

        Protected animationTime As Integer = 5
        Protected currentAnimationTime As Integer = 0
        Protected Overrides Sub Tick()
            ' Animation pour image
            If currentAnimationTime = 0 Then
                If listImage.Count > currentImageIndex + 1 Then
                    SetCurrentImage(currentImageIndex + 1)
                Else
                    SetCurrentImage(0)
                    currentImageIndex = 0
                End If
                currentAnimationTime = animationTime
            Else
                currentAnimationTime -= 1
            End If
        End Sub

        Public Overrides Sub Collide(_sprite As AbstractSprite)

        End Sub
    End Class
End Namespace