Imports ZPlateform.Sprites

Public Module SpriteFactory

    ''' <summary>
    ''' Créer une plateforme 
    ''' </summary>
    ''' <param name="_position">Position haut gauche de la plateforme</param>
    ''' <param name="_size">Taille de la plateforme</param>
    ''' <param name="_img">Image de la plateforme</param>
    ''' <returns>Objet plateforme</returns>
    Public Function CreateStaticPlateform(ByVal _position As Point, ByVal _size As Size, Optional _img As Image = Nothing) As StaticPlateform
        Dim tempSprite As ZPlateform.Sprites.AbstractSprite

        tempSprite = New StaticPlateform With {
            .GetPosition = _position,
            .GetSize = _size
        }
        If Not IsNothing(_img) Then
            tempSprite.AddImage(_img)
        End If

        Return CType(tempSprite, StaticPlateform)

    End Function

    Public Function CreateMovingPlateform(ByVal _position As Point, ByVal _size As Size, Optional _img As Image = Nothing) As MovingPlateform
        Dim tempSprite As ZPlateform.Sprites.MovingPlateform

        tempSprite = New MovingPlateform With {
            .GetPosition = _position,
            .GetSize = _size
        }

        tempSprite.AddNode(_position.X, _position.Y, 1, 0)
        If Not IsNothing(_img) Then
            tempSprite.AddImage(_img)
        End If

        Return tempSprite
    End Function

    Public Function CreateStaticBackgroundItem(ByVal _position As Point, ByVal _size As Size, Optional _img As Image = Nothing) As StaticBackgroundItem
        Dim tempSprite As ZPlateform.Sprites.StaticBackgroundItem

        tempSprite = New StaticBackgroundItem With {
            .GetPosition = _position,
            .GetSize = _size
        }

        If Not IsNothing(_img) Then
            tempSprite.AddImage(_img)
        End If

        Return tempSprite
    End Function

    'Public Function CreateFloatingText()

    ' End Function


End Module
