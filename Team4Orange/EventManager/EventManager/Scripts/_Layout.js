var loggedIn = false;

var signIn = function (username, password) {
    //query db with creds to check login
    loggedIn = true;
    return false;
}

var signOut = function() {
    loggedIn = false;
    return false;
}