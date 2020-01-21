''' <summary>
''' Gère les dialogues de jeu
''' </summary>
Public Class GameDialogManager
    ''' <summary>
    ''' GameEngine parent
    ''' </summary>
    Private ReadOnly parent As GameEngine
    ''' <summary>
    ''' Liste contenant les dialogues
    ''' </summary>
    Private listDialogs As List(Of GameDialog)
    ''' <summary>
    ''' Dernier dialogue créé
    ''' </summary>
    Private lastCreatedDialog As GameDialog
    ''' <summary>
    ''' Indique à GameView si il doit dessiner le dialogue
    ''' </summary>
    Private drawDialog As Boolean
    ''' <summary>
    ''' Message en cours
    ''' </summary>
    Public currentMessage As GameMessageDialog
    ''' <summary>
    ''' Texte que GameView doit dessiner
    ''' </summary>
    Private currentTextToDraw As String
    ''' <summary>
    ''' Dialogue en cours de lecture
    ''' </summary>
    Private currentDialog As GameDialog
    ''' <summary>
    ''' Index du message en cours de lecture
    ''' </summary>
    Private currentIndex As Integer
    ''' <summary>
    ''' Tick supplémentaire avant fin de dialogue
    ''' </summary>
    Private currentExtraTick As Integer = 0
    ''' <summary>
    ''' Touche clavier pour passer le message
    ''' </summary>
    Private SkipMessageKey As Keys
    ''' <summary>
    ''' Touche clavier pour passer le dialogue
    ''' </summary>
    Private SkipDialogKey As Keys

    ''' <summary>
    ''' Créer le GameDialogManager
    ''' </summary>
    ''' <param name="_parent">GameEngine</param>
    Sub New(ByVal _parent As GameEngine)
        currentIndex = 0
        listDialogs = New List(Of GameDialog)
        parent = _parent
        ' Options par défaut
        SkipMessageKey = Keys.Enter
        SkipDialogKey = Keys.Escape
    End Sub

    ''' <summary>
    ''' Ajoute un dialogue
    ''' </summary>
    ''' <param name="_dialog">Dialogue contenant les messages</param>
    Public Sub AddDialog(ByVal _dialog As GameDialog)
        If Not IsNothing(_dialog) Then
            If Not NameExist(_dialog.GetName) Then
                listDialogs.Add(_dialog)
                lastCreatedDialog = _dialog
            End If
        End If
    End Sub

    ''' <summary>
    ''' Créer un dialogue et l'ajoute directement dans la liste des dialogues
    ''' </summary>
    ''' <param name="_name"></param>
    Public Sub CreateDialog(ByVal _name As String)
        ' Si le nom de dialogue n'existe pas encore
        If Not NameExist(_name) Then
            Dim temp As New GameDialog With {
                .GetName = _name
            }
            listDialogs.Add(temp)
            lastCreatedDialog = temp
        End If
    End Sub

    ''' <summary>
    ''' Vérifie si le nom de dialogue existe déjà
    ''' </summary>
    ''' <param name="_name">Nom du dialogue</param>
    ''' <returns>True si il existe sinon retourne false</returns>
    Public Function NameExist(ByVal _name As String) As Boolean
        For Each _dialog As GameDialog In listDialogs
            If _dialog.GetName = _name Then
                Return True
            End If
        Next
        Return False
    End Function

    ''' <summary>
    ''' Ajoute un message au dernier dialogue qui a été créé
    ''' </summary>
    ''' <param name="_message">Message de dialogue</param>
    Public Sub AddMessageToLastCreatedDialog(ByVal _message As GameMessageDialog)
        If Not IsNothing(lastCreatedDialog) Then
            lastCreatedDialog.AddMessage(_message)
        End If
    End Sub

    ''' <summary>
    ''' Supprime un dialogue
    ''' </summary>
    ''' <param name="_dialog">Instance du dialogue à supprimer</param>
    Public Sub RemoveDialog(ByVal _dialog As GameDialog)
        If listDialogs.Contains(_dialog) Then
            listDialogs.Remove(_dialog)
        End If
    End Sub

    ''' <summary>
    ''' Supprime un dialogue selon son nom
    ''' </summary>
    ''' <param name="_name">Nom du dialogue</param>
    Public Sub RemoveDialogByName(ByVal _name As String)
        Dim toRemove As GameDialog = Nothing
        For Each _dialog As GameDialog In listDialogs
            If _dialog.GetName = _name Then
                toRemove = _dialog
                Exit For
            End If
        Next
        If Not IsNothing(toRemove) Then
            listDialogs.Remove(toRemove)
        End If
    End Sub

    ''' <summary>
    ''' Affiche un dialogue
    ''' </summary>
    ''' <param name="_name">Nom du dialogue</param>
    Public Sub ShowDialog(ByVal _name As String)
        For Each _dialog As GameDialog In listDialogs
            If _dialog.GetName = _name Then
                currentIndex = 0
                currentDialog = _dialog
                currentTextToDraw = ""
                currentMessage = _dialog.GetMessageByIndex(0)
                drawDialog = True
                currentExtraTick = 0
            End If
        Next
    End Sub

    ''' <summary>
    ''' Arrête immédiatement le dialogue en cours
    ''' </summary>
    Public Sub AbortDialog()

    End Sub

    ''' <summary>
    ''' Horloge pour dialogue
    ''' </summary>
    Public Sub Tick()
        ' S'il y'a un message à écrire
        If Not IsNothing(currentMessage) Then
            If currentTextToDraw.Count < currentMessage.text.Count Then
                currentTextToDraw = currentTextToDraw + currentMessage.text.Substring(currentTextToDraw.Count, 1)
            Else
                currentExtraTick = currentExtraTick + 1
            End If

            If currentExtraTick = currentMessage.extraTick And currentMessage.skipAutoAfter Then
                NextMessage()
            End If
        End If

    End Sub

    ''' <summary>
    ''' Event clavier pour passer les dialogues
    ''' </summary>
    ''' <param name="_key"></param>
    Public Sub KeyDown(ByVal _key As Keys)

        If _key = SkipMessageKey Then
            NextMessage()
        ElseIf _key = SkipDialogKey Then

        End If
    End Sub

    ''' <summary>
    ''' Passe au message de dialogue suivant
    ''' </summary>
    Protected Sub NextMessage()
        ' Si l'index suivant contient un message
        If Not currentDialog.IsLastIndex(currentIndex + 1) Then
            ' Reset le texte à écrire
            currentTextToDraw = ""
            currentIndex = currentIndex + 1
            currentMessage = currentDialog.GetMessageByIndex(currentIndex)
            currentExtraTick = 0
        Else
            ' Sinon on arrive à la fin du dialogue
            currentMessage = Nothing
            drawDialog = False
        End If
    End Sub

    Public Sub Dispose()

    End Sub

    ''' <summary>
    ''' Indique si il faut dessiner un dialogue
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property GetDrawDialog As Boolean
        Get
            Return drawDialog
        End Get
    End Property

    ''' <summary>
    ''' Récupère le message a dessiner
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property GetMessageToDraw As GameMessageDialog
        Get
            Return currentMessage
        End Get
    End Property

    ''' <summary>
    ''' Retourne le nombre de dialogue contenu dans GameDialogManager
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property DebugDialogCount As Integer
        Get
            Return listDialogs.Count
        End Get
    End Property

    ''' <summary>
    ''' Retourne le texte qui doit être dessiné
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property GetTextToDraw As String
        Get
            Return currentTextToDraw
        End Get
    End Property
End Class

''' <summary>
''' Est un dialogue
''' Contient une liste de messages de dialogue
''' </summary>
Public Class GameDialog

    Protected name As String
    Protected listMessageDialog As List(Of GameMessageDialog)
    Protected canSkip As Boolean

    Sub New()
        listMessageDialog = New List(Of GameMessageDialog)
    End Sub
    ''' <summary>
    ''' Ajoute un message au dialogue
    ''' </summary>
    ''' <param name="_message">Message</param>
    Public Sub AddMessage(ByVal _message As GameMessageDialog)
        If Not IsNothing(_message) Then
            listMessageDialog.Add(_message)
        End If
    End Sub

    ''' <summary>
    ''' Retourne ou modifie le nom du dialogue
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
    ''' Le dialogue peut-être passé directement
    ''' </summary>
    ''' <returns></returns>
    Public Property GetCanFullSkip As Boolean
        Get
            Return canSkip
        End Get
        Set(value As Boolean)
            canSkip = value
        End Set
    End Property

    ''' <summary>
    ''' Indique si l'index est le dernier de la liste de message du dialogue
    ''' </summary>
    ''' <param name="_index"></param>
    ''' <returns></returns>
    Public ReadOnly Property IsLastIndex(ByVal _index As Integer) As Boolean
        Get
            If listMessageDialog.Count < _index + 1 Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    ''' <summary>
    ''' Retourne un message de dialogue selon l'index
    ''' </summary>
    ''' <param name="_index">Index de la liste</param>
    ''' <returns>Message de dialogue</returns>
    Public ReadOnly Property GetMessageByIndex(ByVal _index As Integer) As GameMessageDialog
        Get
            If listMessageDialog.Count > _index Then
                Return listMessageDialog(_index)
            End If
            Return Nothing
        End Get
    End Property
End Class

''' <summary>
''' Est un message de dialogue
''' </summary>
Public Class GameMessageDialog

    ''' <summary>
    ''' Type de dialog
    ''' </summary>
    Public Enum TYPEDIALOG
        TEXTE_ONLY
        TEXT_AND_TITLE
        LEFT_IMAGE
        RIGHT_IMAGE
    End Enum

    Private dialogType As TYPEDIALOG

    ''' <summary>
    ''' Couleur de fond générale
    ''' </summary>
    Public backgroundColor As Color = Color.White
    ''' <summary>
    ''' Texte qui sera affiché pendant le dialogue
    ''' </summary>
    Public text As String = "Text"
    ''' <summary>
    ''' Police du texte du dialogue
    ''' </summary>
    Public textFont As Font = New Font("Arial", 12)
    ''' <summary>
    ''' Couleur du texte du dialogue
    ''' </summary>
    Public textColor As Color = Color.Black
    ''' <summary>
    ''' Couleur d'arrière plan du dialogue
    ''' </summary>
    Public textBackgroundColor As Color = Color.White
    ''' <summary>
    ''' Titre du dialogue
    ''' </summary>
    Public title As String = "Title"
    ''' <summary>
    ''' Police du titre du dialogue
    ''' </summary>
    Public titleFont As Font = New Font("Arial", 12, FontStyle.Bold)
    ''' <summary>
    ''' Couleur du titre du dialogue
    ''' </summary>
    Public titleColor As Color = Color.Black
    ''' <summary>
    ''' Couleur de fond du titre du dialogue
    ''' </summary>
    Public titleBackgroundColor As Color = Color.White
    ''' <summary>
    ''' Image du dialog
    ''' </summary>
    Public image As Image = Nothing
    ''' <summary>
    ''' Le message peut-être passé
    ''' </summary>
    Public canSkip As Boolean = False
    ''' <summary>
    ''' Le message passe automatiquement après la lecture
    ''' </summary>
    Public skipAutoAfter As Boolean = True
    ''' <summary>
    ''' Le texte est affiché instantanément
    ''' </summary>
    Public instantText As Boolean = False
    ''' <summary>
    ''' Tick supplémentaire après affichage du dialogue
    ''' </summary>
    Public extraTick As Integer = 20
End Class
