function getFilesUrl() {
    host = process.env.NODE_ENV === "development" ?  "https://localhost:44380/":"/shop.api/"; 
    return host; 
}

