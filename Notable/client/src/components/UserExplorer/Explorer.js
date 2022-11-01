import React, { useState } from "react";
import { Button, Input } from "reactstrap";
import { useNavigate } from "react-router-dom";
import { _getMyNotes, _getPublicNotes } from "../../modules/noteManager";

export default function Explorer(){
    const nav = useNavigate()
    const [notes, setNotes] = useState([])
    const [search, setSearch] = useState("")

    const updateNotes = () => {
        _getPublicNotes(search).then((_notes) => {
            _notes = _notes.sort(function(a,b){
                return new Date(b.createdAt) - new Date(a.createdAt);
            });

            setNotes(_notes)
        })
    }

    useState(() => {
        updateNotes();
    })


    return (<>
         <div style={{position:"absolute",top:"135px", left:"15%", paddingRight:"100px"}}>
         <Input onChange={(e) => {setSearch(e.target.value)}} style={{position:"absolute",left:"0px", width:"150px"}}></Input>
         <Button onClick={() => {updateNotes()}} style={{position:"absolute",left:"160px"}}>Search</Button>
         </div>
         <table className="cleantable">
            <thead>
                <tr>
                    <td><b>Creator</b></td>
                    <td><b>Name</b></td>
                    <td><b>Date Added</b></td>
                    <td><b>View</b></td>
                </tr>
            </thead>
            <tbody>
                {notes.map((note) => 
                (<tr key={note.id} className="dataRow">
                    <td>{note.userProfile.username}</td>
                    <td>{note.name}</td>
                    <td>{new Date(note.createdAt).toLocaleDateString()}</td>
                    <td>
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
    </>)
}