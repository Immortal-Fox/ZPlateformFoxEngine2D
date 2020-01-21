Namespace Sprites

    ''' <summary>
    ''' Plateforme mouvante
    ''' </summary>
    Public Class MovingPlateform
        Inherits AbstractSprite
        Protected Activated As Boolean

        Protected listNodes As New List(Of MovingPlateformNode)
        Protected numberOfNode As Integer = 0
        Protected currentNode As Integer = 0
        Protected isMoving As Boolean = False

        Sub New()
            collisionOn = True
        End Sub

        Protected Overrides Sub Tick()
            If listNodes.Count > 0 Then
                Dim tempNode As MovingPlateformNode = listNodes(currentNode)
                ' Si la plateforme est a destination
                If tempNode.xEnd = Me.GetX And tempNode.yEnd = Me.GetY Then
                    ' Si il y a encore un node à parcourir
                    If listNodes.Count > currentNode + 1 Then
                        ' On incrémente la valeur
                        currentNode = currentNode + 1
                    Else
                        currentNode = 0
                    End If

                Else
                    ' Sinon on déplace la plateforme
                    If tempNode.xEnd > Me.GetX Then
                        Me.GetX = Me.GetX + tempNode.speed
                        UpdateHitBox()
                    End If
                    If tempNode.xEnd < Me.GetX Then
                        Me.GetX = Me.GetX - tempNode.speed
                        UpdateHitBox()
                    End If
                    If tempNode.yEnd < Me.GetY Then
                        Me.GetY = Me.GetY - tempNode.speed
                        UpdateHitBox()
                    End If
                    If tempNode.yEnd > Me.GetY Then
                        Me.GetY = Me.GetY + tempNode.speed
                        UpdateHitBox()
                    End If
                End If

            End If

        End Sub

        Public Overrides Sub Collide(_sprite As AbstractSprite)

        End Sub

        ''' <summary>
        ''' Ajoute un point de passage pour la plateforme
        ''' </summary>
        ''' <param name="_xEnd"></param>
        ''' <param name="_yEnd"></param>
        ''' <param name="_speed"></param>
        ''' <param name="_breakTime"></param>
        Public Sub AddNode(ByVal _xEnd As Integer,
                           ByVal _yEnd As Integer,
                           ByVal _speed As Integer,
                           ByVal _breakTime As Integer)

            Dim tempNode As MovingPlateformNode
            tempNode.xEnd = _xEnd
            tempNode.yEnd = _yEnd
            tempNode.speed = _speed
            tempNode.breakTime = _breakTime
            listNodes.Add(tempNode)
            numberOfNode = listNodes.Count
        End Sub


        Protected Structure MovingPlateformNode
            Public xEnd As Integer
            Public yEnd As Integer
            Public speed As Integer
            Public breakTime As Integer
        End Structure

    End Class

End Namespace
