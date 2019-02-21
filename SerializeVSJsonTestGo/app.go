package main

import (
	"fmt"
	"log"
	"net/http"
	"os"
	"time"
)

func main() {
	start := time.Now()
	for i := 1; i <= 10000; i++ {
		_, err := http.Get("http://localhost:5000/home/getgenerated")
		if err != nil {
			fmt.Printf(err.Error())
			os.Exit(1)
		}
	}
	elapsed := time.Since(start)
	log.Printf("Generated test took %s", elapsed)

	start = time.Now()
	for i := 1; i <= 10000; i++ {
		_, err := http.Get("http://localhost:5000/home/gettemplate")
		if err != nil {
			fmt.Printf(err.Error())
			os.Exit(1)
		}
	}

	elapsed = time.Since(start)
	log.Printf("Template test took %s", elapsed)

}
