import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { Button, ButtonToggle, Form, Input, Label, Modal, ModalHeader, ModalBody, ModalFooter,
Card, CardBody, CardHeader,  CardFooter} from "reactstrap";
import { _addNote, _deleteNote, _getMyNotes } from "../../modules/noteManager";
import "../styles/noteList.css"


export default function NoteList(){
    const [notes, setNotes] = useState([])

    const [showCreateModal, setShowCreateModal] = useState(false);
    const [noteName, setNoteName] = useState("Untitled Note")
    const [noteContent, setNoteContent] = useState("Some Text...")
    const [notePublic, setNotePublic] = useState(false)

    const [showDelModal, setShowDelModal] = useState(false)
    const [delNoteId,setDelNoteId] = useState(null)
    const [delNoteName,setDelNoteName] = useState("")

    const nav = useNavigate()

    
    const toggleCreateModal = () => {
        setShowCreateModal(!showCreateModal)
    }

    const toggleDeleteModal = () => {
        setShowDelModal(!showDelModal)
    }

    const updateNotes = () => {
        _getMyNotes().then((_notes) => {
            _notes = _notes.sort(function(a,b){
                return new Date(b.createdAt) - new Date(a.createdAt);
            });

            setNotes(_notes)
        })
    }

    const AddNote = () => {
        const obj = {name:noteName, content:noteContent, isPublic:notePublic};
        _addNote(obj)
        .then(() => {updateNotes();});
    }

    useState(() => {
        updateNotes();
    })

    return (<>
    <div className="AddNoteButton">
        <Button style={{marginRight:"10px"}} onClick={() => nav(-1)}>Back</Button>
        <Button onClick={() => {setShowCreateModal(true)}}>Create a Note</Button>
    </div>

    <table className="notesTable">
            <thead>
                <tr>
                    <td><b>Name</b></td>
                    <td><b>Visibility</b></td>
                    <td><b>Date Added</b></td>
                    <td><b>Manage</b></td>
                </tr>
            </thead>
            <tbody>
                {notes.map((note) => 
                (<tr key={note.id} className="dataRow">
                    <td>{note.name}</td>
                    <td>{(note.isPublic ? "Public":"Private")}</td>
                    <td>{new Date(note.createdAt).toLocaleDateString()}</td>
                    <td>
                        <Button 
                        onClick={() => {setDelNoteId(note.id); setDelNoteName(note.name); toggleDeleteModal()}} 
                        className="optionButton"><img src="/trash.svg" /></Button>

                        <Button 
                        onClick={() => nav(`/notes/${note.id}`)} 
                        className="optionButton"><img src="/eye.svg" /></Button>
                        {/*
                        <Button className="optionButton"><img src="/chat.svg" /></Button>
                        <ButtonToggle className="optionButton"><img src="/hand-thumbs-up.svg" /></ButtonToggle> 
                        */}
                    </td>
                </tr>)
                )}
            </tbody>
        </table>



        <Modal className="CreateModal" isOpen={showCreateModal} toggle={() => setShowCreateModal(!showDelModal)}>

         <ModalHeader className="darkmodal" toggle={toggleCreateModal}>
            <div style={{textAlign:"center"}}><h3>Create a Note</h3></div>
         </ModalHeader>
        

          <ModalBody className="darkmodal">
          <Form>
                <Label for="Name">Name</Label>
                <Input
                style={{backgroundColor:"#454545",borderColor:"#454545", color:"white"}}
                maxLength="15"
                autoComplete="off"
                id="name"
                type="text"
                autoFocus
                onChange={(e) =>{setNoteName(e.target.value)}}/>
                <Label for="name">Content</Label>
                <Input
                style={{height:"200px", backgroundColor:"#454545",borderColor:"#454545", color:"white"}}
                maxLength="999"
                autoComplete="off"
                id="content"
                type="textarea"
                autoFocus
                onChange={(e) =>{setNoteContent(e.target.value)}}/>
                <Label for="email">Public <input onChange={(e) => {setNotePublic(e.target.checked)}} type={"checkbox"}></input></Label>
                <div style={{textAlign:"center"}}>
                    <Button color="success" onClick={() => {
                        AddNote()
                        setShowCreateModal(false)
                        document.querySelector(".NoteForm").reset()
                        setNoteName("Untitled Note");
                        setNoteContent("Some Text...");
                        setNotePublic(false);
                    }}>Submit</Button>
                </div>
            </Form>
          </ModalBody>
        </Modal>



        <Modal className="delModal" color="dark" isOpen={showDelModal} toggle={() => setShowDelModal(!showDelModal)}>
          <ModalHeader className="darkmodal" toggle={toggleDeleteModal}>Delete Note</ModalHeader>
          <ModalBody className="darkmodal">
            Are you sure you want to delete "{delNoteName}"?
            <div>
                This action cannot be undone.
            </div>
          </ModalBody>
          <ModalFooter className="darkmodal">
            <Button color="danger" onClick={() => {
                _deleteNote(delNoteId).then(() => updateNotes()).then(() => {toggleDeleteModal(); })  
            }}>Delete</Button>{' '}
            <Button color="secondary" onClick={toggleDeleteModal}>Cancel</Button>
          </ModalFooter>
        </Modal>

    </>)
    
}