
''' <summary>
''' Gère l'affichage sur le contrôle graphique souhaité
''' </summary>
Public Class GameViewManager
    ' GameEngine parent
    Private parent As GameEngine

    ' Contrôle pour l'affichage du jeu
    Private pboxView As PictureBox
    ' Image dessinée
    Private bmpView As Bitmap

    ' Graphics pour le dessin sur l'image
    Private gView As Graphics
    'Private cameraSize As Size

    ' Debug info
    Private debugFPS As Integer
    Private debugLastTimeUpdate As Date

    ' Liste de caméra
    Private ReadOnly listCamera As New List(Of Camera)
    ' Camra courante
    'Private currentCamera As Camera

    ' Rectangles pour le dessin du dialogue
    Private rectDialogFull As Rectangle
    Private rectDialogLeft As Rectangle
    Private rectDialogTitle As Rectangle
    Private rectDialogImage As Rectangle
    Private rectDialogText As Rectangle

    Sub New(ByVal _parent As GameEngine)
        parent = _parent
        'cameraSize = New Size(584, 561)
    End Sub

    ''' <summary>
    ''' Définit le control (Picturebox) sur lequel afficher l'image du jeu
    ''' </summary>
    ''' <param name="_viewControl"></param>
    Public Sub SetViewControl(ByVal _viewControl As PictureBox)
        pboxView = _viewControl
    End Sub

    ''' <summary>
    ''' Ajoute une camera dans la liste de camera
    ''' </summary>
    ''' <param name="_camera">Objet caméra</param>
    Public Sub AddCamera(ByVal _camera As Camera)
        listCamera.Add(_camera)
    End Sub

    ''' <summary>
    ''' Retourne une camera selon l'index
    ''' </summary>
    ''' <param name="_index">Index de la caméra</param>
    ''' <returns>Caméra</returns>
    Public Function GetCameraByIndex(ByVal _index As Integer) As Camera
        If listCamera.Count > _index Then
            Return listCamera(_index)
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' Retourne une camera selon un nom
    ''' </summary>
    ''' <param name="_name">Nom de la caméra</param>
    ''' <returns>Caméra</returns>
    Public Function GetCameraByName(ByVal _name As String) As Camera
        For Each _camera As Camera In listCamera
            If _camera.GetName = _name Then
                Return _camera
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' Supprime une caméra
    ''' </summary>
    ''' <param name="_camera">Caméra à supprimer</param>
    Public Sub RemoveCamera(ByVal _camera As Camera)
        If listCamera.Contains(_camera) Then
            listCamera.Remove(_camera)
        End If
    End Sub

    Public Sub SendCameraToBack(ByVal _camera As Camera)

    End Sub

    ''' <summary>
    ''' Initialise les composants graphiques pour le dessin
    ''' </summary>
    ''' <param name="_viewSize"></param>
    Friend Sub InitializeGraphics(ByVal _viewSize As Size)
        ' Si l'image existe déjà, on l'a détruit
        If Not IsNothing(bmpView) Then
            bmpView.Dispose()
        End If
        ' Créer la nouvelle image
        bmpView = New Bitmap(_viewSize.Width, _viewSize.Height)
        ' Initialise le graphic sur l'image
        gView = Graphics.FromImage(bmpView)

        ' Initialisation des rectangles pour le dessin du dialogues
        rectDialogFull = New Rectangle(0, CInt((_viewSize.Height / 3) * 2), _viewSize.Width, CInt(_viewSize.Height / 3))
        rectDialogLeft = New Rectangle(rectDialogFull.Location, New Size(CInt(rectDialogFull.Width / 4), rectDialogFull.Height))
        rectDialogTitle = New Rectangle(rectDialogLeft.Location, New Size(rectDialogLeft.Width, CInt(rectDialogLeft.Height / 9.5)))
        rectDialogImage = New Rectangle(0, rectDialogLeft.Y + rectDialogTitle.Height, rectDialogLeft.Width, CInt(rectDialogLeft.Height * 0.9))
        rectDialogText = New Rectangle(rectDialogLeft.Width, rectDialogLeft.Y, rectDialogFull.Width - rectDialogLeft.Width, rectDialogLeft.Height)
    End Sub

    ''' <summary>
    ''' Redessine l'intégralité de la fenêtre de jeu
    ''' Tous les sprites visibles seront dessiné
    ''' </summary>
    Public Sub DrawView()

        ' Si l'image n'existe pas on quitte la méthode
        If IsNothing(bmpView) Then
            Exit Sub
        End If

        ' On clear l'image
        gView.Clear(Color.Azure)

        ' On parcours la liste des caméras
        For Each _camera As Camera In listCamera
            ' Si la caméra est activée
            If _camera.GetEnabled Then
                ' On récupère la liste des sprites qui collisionnent avec le rectangle de la caméra
                For Each _sprite As Sprites.AbstractSprite In parent.SpritesManagement.GetInSceneSprites(_camera.GetRectangle)
                    '  If _sprite.getVisible Then todo
                    ' Dessine l'image
                    gView.DrawImage(CType(_sprite.GetCurrentImage, Image), New Rectangle(_sprite.GetPosition.X - _camera.GetRectangle.X, _sprite.GetPosition.Y - _camera.GetRectangle.Y, _sprite.GetSize.Width, _sprite.GetSize.Height))
                    ' Si le mode debug est demandé on affiche également les hitbox
                    If parent.DebugManagement.ShowDebugHitbox Then
                        gView.DrawRectangle(New Pen(Color.Black, 1), New Rectangle(_sprite.GetPosition.X - _camera.GetRectangle.X, _sprite.GetPosition.Y - _camera.GetRectangle.Y, _sprite.GetSize.Width, _sprite.GetSize.Height))
                    End If
                Next
            End If
        Next

        ' Ensuite dessine l'interface de jeu
        If parent.ScreenElementsManagement.GetListScreenElements.Count > 0 Then
            For Each _screenElement As ScreenElements.AbstractScreenElement In parent.ScreenElementsManagement.GetListScreenElements
                If Not IsNothing(_screenElement) Then
                    gView.DrawImageUnscaledAndClipped(_screenElement.GetImage, New Rectangle(_screenElement.GetPosition, _screenElement.GetSize))
                End If
            Next
        End If

        ' Ensuite on dessine le dialogue
        If True Then
            If Not IsNothing(parent.DialogManagement.GetMessageToDraw) Then
                Dim temp As GameMessageDialog = parent.DialogManagement.GetMessageToDraw
                Dim sf As New StringFormat With {
                    .Alignment = StringAlignment.Center
                }

                'If parent.DialogManagement.GetDrawDialog Then
                If Not IsNothing(parent.DialogManagement.GetMessageToDraw) Then
                    ' Dessine l'arrière plan du dialogue
                    gView.FillRectangle(New SolidBrush(temp.backgroundColor), rectDialogFull)
                    ' Dessine le rectangle gauche contenant titre et image
                    gView.FillRectangle(New SolidBrush(temp.backgroundColor), rectDialogLeft)
                    ' Dessine le rectangle contenant le titre
                    gView.FillRectangle(New SolidBrush(temp.titleBackgroundColor), rectDialogTitle)
                    ' Dessine le titre
                    gView.DrawString(temp.title, temp.titleFont, New SolidBrush(temp.titleColor), rectDialogTitle, sf)
                    ' Dessine le rectangle contenant l'image
                    gView.FillRectangle(New SolidBrush(Color.Red), rectDialogImage)
                    ' Dessine l'image
                    If Not IsNothing(temp.image) Then
                        gView.DrawImageUnscaledAndClipped(temp.image, rectDialogImage)
                    End If
                    ' Dessine le rectangle contenant le texte
                    gView.FillRectangle(New SolidBrush(temp.textBackgroundColor), rectDialogText)
                    ' Dessine le texte
                    gView.DrawString(parent.DialogManagement.GetTextToDraw, temp.textFont, New SolidBrush(temp.textColor), rectDialogText, sf)
                End If
            End If
        End If

        If parent.DebugManagement.ShowDebugTexte Then
            gView.DrawString(CStr(parent.DebugManagement.GetTranslatedDebugText), New Font("Consolas", 9, FontStyle.Bold), New SolidBrush(Color.Black), 0, 0)
        End If
        ' Met à jour le contrôle d'affichage
        If Not IsNothing(pboxView.Image) Then
            pboxView.Image.Dispose()
        End If
        pboxView.Image = CType(bmpView.Clone, Image)
        If Not IsNothing(debugLastTimeUpdate) Then
            debugFPS = CInt(1000 / (Date.Now.Subtract(debugLastTimeUpdate).TotalMilliseconds + 1))
        End If
        debugLastTimeUpdate = Date.Now
    End Sub


    Public Property GetSceneSize As Size
        Get

        End Get
        Set(value As Size)

        End Set
    End Property

    Public ReadOnly Property GetDebugFPS As Integer
        Get
            Return debugFPS
        End Get
    End Property
End Class
