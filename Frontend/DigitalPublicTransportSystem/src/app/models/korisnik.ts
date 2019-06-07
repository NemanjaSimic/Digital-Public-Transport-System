export class Korisnik {

    username: string;
    name: string;
    surname: string;
    email: string;
    password: string;
    confirmPassword: string;
    address: string;
    dateOfBirth: Date;
    userType: string;
    imgUrl: string;

    constructor(username?: string, firstName?: string, lastName?: string, email?: string, 
                password?: string, confirmPassword?: string, address?: string, dob?: Date, userType?: string, imgUrl?: string) {

        this.username = username;
        this.name = firstName;
        this.surname = lastName;
        this.email = email;
        this.password = password;
        this.confirmPassword = confirmPassword;
        this.address = address;
        this.dateOfBirth = dob;
        this.userType = userType;
        this.imgUrl = imgUrl;
    }
}