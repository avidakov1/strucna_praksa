import React, { Component } from "react";
import axios from "axios";

class People extends Component {
  constructor() {
    super();
    this.state = {
      people: [],
    };
  }

  onClickHandle = () => {};

  componentDidMount = () => {
    axios.get("api/Person").then((response) => {
      this.setState({
        people: [...this.state.people, ...response.data],
      });
      console.log(response);
    });
  };

  render() {
    return (
      <React.Fragment>
        <table>
          <thead>
            <tr>
              <th>Firstname</th>
              <th>Lastname</th>
              <th>DOB</th>
              <th>Izmjeni</th>
              <th>Obriši</th>
            </tr>
          </thead>
          <tbody>
            {this.state.people.map((people) => (
              <tr key={people.Id}>
                <td>{people.FirstName}</td>
                <td>{people.LastName}</td>
                <td>{people.DOB.substring(0, 10)}</td>
                <td>
                  <button className="btn btn-primary">Izmjeni</button>
                </td>
                <td>
                  <button className="btn btn-danger">Obriši</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </React.Fragment>
    );
  }
}

export default People;
