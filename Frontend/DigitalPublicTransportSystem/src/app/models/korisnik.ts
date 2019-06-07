export class Korisnik {

    username: string;
    firstName: string;
    lastName: string;
    email: string;
    password: string;
    confirmPassword: string;
    address: string;
    dateOfBirth: Date;
    userType: string;

    constructor(username?: string, firstName?: string, lastName?: string, email?: string, 
                password?: string, confirmPassword?: string, address?: string, dob?: Date, userType?: string) {

        this.username = username;
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
        this.password = password;
        this.confirmPassword = confirmPassword;
        this.address = address;
        this.dateOfBirth = dob;
        this.userType = userType;
    }
}