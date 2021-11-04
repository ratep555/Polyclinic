export function toBase64(file: File){
    // promise is a function that will return something in the future
    return new Promise((resolve, reject) => {
        // hover over filereader
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => resolve(reader.result);
        reader.onerror = (error) => reject(error);
    });
}
