import React, { Component } from "react";
import axios from "axios";
import {
  Button,
  Table,
  Modal,
  ModalHeader,
  ModalFooter,
  ModalBody,
  Label,
  Input,
  FormGroup,
} from "reactstrap";

class People extends Component {
  constructor() {
    super();
    this.state = {
      people: [],
      newPersonModal: false,
      editPersonModal: false,
      deletePersonModal: false,
      newPersonData: {
        FirstName: "",
        LastName: "",
        DOB: "",
      },
      editPersonData: {
        FirstName: "",
        LastName: "",
        DOB: "",
      },
      personToManipulateId: "",
    };
  }

  toggleNewPersonModal() {
    this.setState({
      newPersonModal: !this.state.newPersonModal,
    });
  }

  addPerson() {
    axios.post("/api/Person", this.state.newPersonData).then((response) => {
      if (response.data === "OK") {
        this.RefreshTable();
        this.setState({
          newPersonModal: false,
          newPersonData: {
            FirstName: "",
            LastName: "",
            DOB: "",
          },
        });
      }
    });
  }

  RefreshTable = () => {
    axios.get("api/Person").then((response) => {
      this.setState({
        people: [...response.data],
      });
      console.log(response);
    });
  };

  componentWillMount = () => {
    this.RefreshTable();
  };

  render() {
    return (
      <div>
        <h1>Person app</h1>
        <Button
          color="primary"
          onClick={this.toggleNewPersonModal.bind(this)}
          className="m-2"
        >
          Add a new person
        </Button>
        <Modal
          isOpen={this.state.newPersonModal}
          toggle={this.toggleNewPersonModal.bind(this)}
        >
          <ModalHeader toggle={this.toggleNewPersonModal.bind(this)}>
            Add a new person
          </ModalHeader>
          <ModalBody>
            <FormGroup>
              <Label for="firstName">First Name</Label>
              <Input
                type="text"
                name="firstName"
                id="firstName"
                placeholder="First Name"
                value={this.state.newPersonData.FirstName}
                onChange={(e) => {
                  let { newPersonData } = this.state;
                  newPersonData.FirstName = e.target.value;
                  this.setState({ newPersonData });
                }}
              />
            </FormGroup>
            <FormGroup>
              <Label for="lastName">Last Name</Label>
              <Input
                type="text"
                name="lastName"
                id="lastName"
                placeholder="Last Name"
                value={this.state.newPersonData.LastName}
                onChange={(e) => {
                  let { newPersonData } = this.state;
                  newPersonData.LastName = e.target.value;
                  this.setState({ newPersonData });
                }}
              />
            </FormGroup>
            <FormGroup>
              <Label for="dob">Date of birth</Label>
              <Input
                type="text"
                name="dob"
                id="dob"
                placeholder="yyyy-mm-dd"
                value={this.state.newPersonData.DOB}
                onChange={(e) => {
                  let { newPersonData } = this.state;
                  newPersonData.DOB = e.target.value;
                  this.setState({ newPersonData });
                }}
              />
            </FormGroup>
          </ModalBody>
          <ModalFooter>
            <Button color="primary" onClick={this.addPerson.bind(this)}>
              Send
            </Button>{" "}
            <Button
              color="secondary"
              onClick={this.toggleNewPersonModal.bind(this)}
            >
              Cancel
            </Button>
          </ModalFooter>
        </Modal>

        <Modal
          isOpen={this.state.editPersonModal}
          toggle={this.toggleEditPersonModal.bind(this)}
        >
          <ModalHeader toggle={this.toggleEditPersonModal.bind(this)}>
            Edit person
          </ModalHeader>
          <ModalBody>
            <FormGroup>
              <Label for="firstName">First Name</Label>
              <Input
                type="text"
                name="firstName"
                id="firstName"
                placeholder="First Name"
                value={this.state.editPersonData.FirstName}
                onChange={(e) => {
                  let { editPersonData } = this.state;
                  editPersonData.FirstName = e.target.value;
                  this.setState({ editPersonData });
                }}
              />
            </FormGroup>
            <FormGroup>
              <Label for="lastName">Last Name</Label>
              <Input
                type="text"
                name="lastName"
                id="lastName"
                placeholder="Last Name"
                value={this.state.editPersonData.LastName}
                onChange={(e) => {
                  let { editPersonData } = this.state;
                  editPersonData.LastName = e.target.value;
                  this.setState({ editPersonData });
                }}
              />
            </FormGroup>
            <FormGroup>
              <Label for="dob">Date of birth</Label>
              <Input
                type="text"
                name="dob"
                id="dob"
                placeholder="yyyy-mm-dd"
                value={this.state.editPersonData.DOB.substring(0, 10)}
                onChange={(e) => {
                  let { editPersonData } = this.state;
                  editPersonData.DOB = e.target.value;
                  this.setState({ editPersonData });
                }}
              />
            </FormGroup>
          </ModalBody>
          <ModalFooter>
            <Button
              color="primary"
              onClick={this.updatePerson.bind(
                this,
                this.state.personToManipulateId
              )}
            >
              Send
            </Button>{" "}
            <Button
              color="secondary"
              onClick={this.toggleEditPersonModal.bind(this)}
            >
              Cancel
            </Button>
          </ModalFooter>
        </Modal>

        <Modal
          isOpen={this.state.deletePersonModal}
          toggle={this.toggleDeletePersonModal.bind(this)}
        >
          <ModalHeader toggle={this.toggleDeletePersonModal.bind(this)}>
            Delete person
          </ModalHeader>
          <ModalBody>Are you shure you want to delete this person.</ModalBody>
          <ModalFooter>
            <Button
              color="primary"
              onClick={this.deletePersonApi.bind(
                this,
                this.state.personToManipulateId
              )}
            >
              Send
            </Button>{" "}
            <Button
              color="secondary"
              onClick={this.toggleDeletePersonModal.bind(this)}
            >
              Cancel
            </Button>
          </ModalFooter>
        </Modal>

        <Table>
          <thead>
            <tr>
              <th className="text-center">First Name</th>
              <th className="text-center">Last Name</th>
              <th className="text-center">Date of birth</th>
              <th className="text-center">Actions</th>
            </tr>
          </thead>
          <tbody>{this.personFill()}</tbody>
        </Table>
      </div>
    );
  }

  personFill() {
    return this.state.people.map((person) => {
      return (
        <tr key={person.Id}>
          <td>{person.FirstName}</td>
          <td>{person.LastName}</td>
          <td className="text-center">{person.DOB.substring(0, 10)}</td>
          <td className="text-center">
            <Button
              color="primary"
              size="small"
              className="mx-1"
              onClick={this.editPerson.bind(
                this,
                person.Id,
                person.FirstName,
                person.LastName,
                person.DOB
              )}
            >
              Edit
            </Button>
            <Button
              color="danger"
              size="small"
              className="mx-1"
              onClick={this.deletePerson.bind(this, person.Id)}
            >
              Remove
            </Button>
          </td>
        </tr>
      );
    });
  }

  toggleEditPersonModal() {
    this.setState({
      editPersonModal: !this.state.editPersonModal,
    });
  }

  editPerson(id, firstName, lastName, dob) {
    this.setState({
      editPersonModal: !this.state.editPersonModal,
      editPersonData: {
        FirstName: firstName,
        LastName: lastName,
        DOB: dob,
      },
      personToManipulateId: id,
    });
  }

  updatePerson(id) {
    axios
      .put("/api/Person/" + id.toString(), this.state.editPersonData)
      .then((response) => {
        if (response.data === "OK") {
          this.RefreshTable();
          this.setState({
            editPersonModal: false,
            editPersonData: {
              FirstName: "",
              LastName: "",
              DOB: "",
            },
            personToManipulateId: "",
          });
        }

        this.RefreshTable();
      });
  }

  toggleDeletePersonModal() {
    this.setState({
      deletePersonModal: !this.state.deletePersonModal,
    });
  }

  deletePerson(id) {
    this.setState({
      deletePersonModal: !this.state.deletePersonModal,
      personToManipulateId: id,
    });
  }

  deletePersonApi(id) {
    axios.delete("api/Person/" + id.toString()).then((response) => {
      if (response.data === "OK") {
        this.RefreshTable();
        this.setState({
          deletePersonModal: false,
          personToManipulateId: "",
        });
      }

      this.RefreshTable();
    });
  }
}

export default People;
