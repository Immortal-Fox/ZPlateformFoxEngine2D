Namespace Sprites
    ''' <summary>
    ''' Sprite pour le joueur
    ''' </summary>
    Public Class Player
        Inherits Sprites.AbstractSprite

        ' Boutons
        Private keyUpPressed As Boolean
        Private keyDownPressed As Boolean
        Private keyLeftPressed As Boolean
        Private keyRightPressed As Boolean

        Private lateralVelocity As Double
        Private downVelocity As Double
        Private jumpVelocity As Double

        Private isFalling As Boolean
        Private isJumping As Boolean
        Private isMovingLeft As Boolean
        Private isMovingRight As Boolean

        Private collideLeft As Boolean
        Private collideRight As Boolean
        Private collideTop As Boolean
        Private collideBot As Boolean

        Private healthPoints As Integer
        Private maxHealthPoints As Integer
        Private manaPoints As Integer
        Private maxManaPoints As Integer
        Private cameraJoueur As Camera

        ''' <summary>
        ''' Créer un nouveau Joueur
        ''' </summary>
        Sub New()
            GetCollisionOn = True
            healthPoints = 100
            maxHealthPoints = 100
            manaPoints = 100
            maxManaPoints = 100

        End Sub

        Protected Overrides Sub Tick()
            'Créer la caméra du joueur si elle n'existe pas
            If IsNothing(cameraJoueur) Then
                cameraJoueur = New Camera
                parentGameEngine.ViewManagement.AddCamera(cameraJoueur)
                cameraJoueur.GetEnabled = True
            End If
            ' Met à jour les informationa de camera du joueur
            cameraJoueur.GetRectangle = New Rectangle(CInt(GetPosition.X - parentGameEngine.GetSceneSize.Width / 2), CInt(GetPosition.Y - parentGameEngine.GetSceneSize.Height / 2), parentGameEngine.GetSceneSize.Width, parentGameEngine.GetSceneSize.Height)

            collideLeft = False
            collideRight = False
            collideTop = False
            collideBot = False
            Dim collideSpriteLeft As AbstractSprite = Nothing
            Dim collideSpriteRight As AbstractSprite = Nothing
            Dim collideSpriteTop As AbstractSprite = Nothing
            Dim collideSpriteBottom As AbstractSprite = Nothing

            Dim alreadyCollide As New List(Of AbstractSprite)

            ' Check la chut
            Dim listTemp As List(Of AbstractSprite) = parentGameEngine.SpritesManagement.GetCollidingSprites(Me)
            Dim collidingSprite As AbstractSprite


            ' Si la liste est vide (On tombe forcément)
            If listTemp.Count = 0 Then

                If Not isJumping Then
                    If downVelocity < 9 Then
                        downVelocity += 1
                    End If
                    Me.GetY = CInt(Me.GetY + downVelocity + 5)
                    isFalling = True
                End If

                ' Sinon on détermine de quel côté a lieu la collision
            Else
                ' Recherche de collisions à gauche du joueur
                For Each _sprite As AbstractSprite In listTemp
                    collideLeft = CBool(IIf(Me.Left <= _sprite.Right And Me.Left > _sprite.Right - 10, True And _sprite.Top < Me.Bottom - 2, False)) 'Me.Bottom - 1 < _sprite.Top
                    collideSpriteLeft = _sprite
                    _sprite.Collide(Me)

                    If collideLeft Then
                        alreadyCollide.Add(_sprite)
                        Exit For
                    End If

                Next
                ' Recherche de collisions à droite du joueur
                For Each _sprite As AbstractSprite In listTemp
                    collideRight = CBool(IIf(Me.Right >= _sprite.Left And Me.Right < _sprite.Left + 10 And _sprite.Top < Me.Bottom - 2 And Not alreadyCollide.Contains(_sprite), True, False))
                    collideSpriteRight = _sprite
                    _sprite.Collide(Me)
                    If collideRight Then
                        alreadyCollide.Add(_sprite)
                        Exit For
                    End If
                Next
                ' Recherche de collisions en bas du joueur
                For Each _sprite As AbstractSprite In listTemp
                    collideBot = CBool(IIf(Me.Bottom >= _sprite.Top And Me.Bottom < _sprite.Top + 15 And _sprite.Left < Me.Right And _sprite.Right > Me.Left And Not alreadyCollide.Contains(_sprite), True, False))
                    collideSpriteBottom = _sprite
                    _sprite.Collide(Me)
                    If collideBot Then
                        alreadyCollide.Add(_sprite)
                        Exit For
                    End If
                Next
                ' Recherche de collisions en haut du joueur
                For Each _sprite As AbstractSprite In listTemp
                    collideTop = CBool(IIf(Me.Top <= _sprite.Bottom And Me.Top > _sprite.Bottom - 15 And Not alreadyCollide.Contains(_sprite), True, False))
                    collideSpriteTop = _sprite
                    _sprite.Collide(Me)
                    If collideTop Then
                        alreadyCollide.Add(_sprite)
                        Exit For
                    End If
                Next
                collidingSprite = listTemp.First

                If collideBot Then
                    Me.GetY = collideSpriteBottom.Top - Me.GetSize.Height + 1
                    downVelocity = 0
                    isFalling = False
                ElseIf (Not isJumping) Then

                    If downVelocity < 3 Then
                        downVelocity += 1
                    End If
                    Me.GetY = CInt(Me.GetPosition.Y + downVelocity + 3)
                    isFalling = True
                End If
            End If

            If keyUpPressed Then
                ' Update
                If isFalling Then
                    isJumping = False
                ElseIf Not isJumping Then
                    jumpVelocity = 15
                    isJumping = True
                End If
                If isJumping Then
                    If Not collideTop Then
                        If jumpVelocity > 15 Then
                            Me.GetY -= 12
                        Else
                            Me.GetY = CInt(Me.GetY - jumpVelocity)
                        End If

                        jumpVelocity -= 1
                        If jumpVelocity < downVelocity Then
                            isJumping = False
                            isFalling = True
                        End If
                    Else
                        isJumping = False
                        isFalling = True
                    End If
                End If
            Else
                isJumping = False
            End If

            'If keyUpPressed Then
            '    ' Si le joueur est sur une plateforme
            '    If collideBot Then
            '        If Not isJumping Then
            '            isJumping = True
            '            jumpVelocity = 9
            '            Me.position.Y = Me.position.Y - 9
            '        Else
            '            Me.position.Y = Me.position.Y - jumpVelocity
            '            jumpVelocity = jumpVelocity - 1
            '            If jumpVelocity = 0 Then
            '                isJumping = False
            '            End If
            '        End If

            '    End If
            'Else
            '    If Not collideBot Then
            '        isJumping = False
            '        isFalling = True
            '    End If
            'End If

            ' La touche left est enfoncée
            If keyLeftPressed Then
                ' Si il n'y a pas de collision à gauche on peut avancer
                If Not collideLeft Then
                    'On déplace le joueur à gauche
                    Me.GetX = CInt(Me.GetX - lateralVelocity)
                    AddLateraleVelocity(1)
                Else
                    Me.GetX = collideSpriteLeft.GetPosition.X + collideSpriteLeft.GetSize.Width - 1
                End If
            Else
                If collideLeft Then
                    Me.GetX = collideSpriteLeft.GetPosition.X + collideSpriteLeft.GetSize.Width - 1
                End If
            End If

            ' La touche left est enfoncée
            If keyRightPressed Then
                ' Si il n'y a pas de collisision à droite
                If Not collideRight Then
                    ' On déplace le joueur à droite
                    Me.GetX = CInt(Me.GetPosition.X + lateralVelocity)
                    AddLateraleVelocity(1)
                Else
                    Me.GetX = collideSpriteRight.GetPosition.X - Me.GetSize.Width + 1
                End If
            Else
                If collideRight Then
                    Me.GetX = collideSpriteRight.GetPosition.X - Me.GetSize.Width + 1
                End If
            End If

            ' Perte de velocité
            If Not keyLeftPressed And Not keyRightPressed Then

                lateralVelocity = 0
            End If

            UpdateHitBox()
            If Me.GetPosition.Y > 3000 Then
                Me.GetY = -200

            End If

        End Sub

        Public Overrides Sub Collide(_sprite As AbstractSprite)

        End Sub

        Protected Sub AddLateraleVelocity(ByVal _velocity As Integer)
            If lateralVelocity + _velocity < 9 Then
                lateralVelocity += _velocity
            End If

        End Sub
        Public Sub KeysDown(ByVal _key As Byte)
            Select Case _key
                Case 1
                    keyLeftPressed = True
                Case 2
                    keyRightPressed = True
                Case 3
                    keyDownPressed = True
                Case 4
                    keyUpPressed = True
            End Select
        End Sub

        Public Sub KeysUp(ByVal _key As Byte)
            Select Case _key
                Case 1
                    keyLeftPressed = False
                Case 2
                    keyRightPressed = False
                Case 3
                    keyDownPressed = False
                Case 4
                    keyUpPressed = False
            End Select
        End Sub

        Public Function GetDebugCode() As String
            Dim texte As String
            texte = "Player" & vbCrLf & "Collide Left = " & collideLeft & vbCrLf
            texte = texte & "Collide Right = " & collideRight & vbCrLf
            texte = texte & "Collide Top = " & collideTop & vbCrLf
            texte = texte & "Collide Bottom = " & collideBot & vbCrLf
            texte = texte & "Position = " & GetPosition.ToString & vbCrLf
            texte = texte & "Velocity Down = " & downVelocity & vbCrLf
            texte = texte & "Velocity Jump = " & jumpVelocity & vbCrLf
            texte = texte & "Velocity Lateral = " & lateralVelocity & vbCrLf
            texte = texte & "Is Falling = " & isFalling & vbCrLf
            texte = texte & "Is Jumping = " & isJumping & vbCrLf
            texte = texte & "W Pressed = " & keyUpPressed & vbCrLf
            texte = texte & "S Pressed = " & keyDownPressed & vbCrLf
            texte = texte & "A Pressed = " & keyLeftPressed & vbCrLf
            texte = texte & "D Pressed = " & keyRightPressed & vbCrLf
            Return texte
        End Function

        ''' <summary>
        ''' Retourne ou modifie les points de vie
        ''' </summary>
        ''' <returns></returns>
        Public Property GetHealthPoints As Integer
            Get
                Return healthPoints
            End Get
            Set(value As Integer)
                healthPoints = value
            End Set
        End Property

        ''' <summary>
        ''' Retourne ou modifie les points de vie maximum
        ''' </summary>
        ''' <returns></returns>
        Public Property GetMaxHealthPoints As Integer
            Get
                Return maxHealthPoints
            End Get
            Set(value As Integer)
                maxHealthPoints = value
            End Set
        End Property

        ''' <summary>
        ''' Retourne ou modifie les points de mana
        ''' </summary>
        ''' <returns></returns>
        Public Property GetManaPoints As Integer
            Get
                Return manaPoints
            End Get
            Set(value As Integer)
                manaPoints = value
            End Set
        End Property

        ''' <summary>
        ''' Retourne ou modifie les points de mana Maximum
        ''' </summary>
        ''' <returns></returns>
        Public Property GetMaxManaPoints As Integer
            Get
                Return maxManaPoints
            End Get
            Set(value As Integer)
                maxManaPoints = value
            End Set
        End Property

    End Class

End Namespace
