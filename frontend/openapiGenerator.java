import java.io.File;
import java.io.IOException;
import java.nio.file.Files;

public class openapiGenerator {

    public static void main(String[] args) {
        String outputDir = "cmsTypeScriptClient";

        // Function to delete directory recursively
        deleteDirectory(new File(outputDir));

        // Run the openapi-generator-cli command
        String command = "java";
        String[] commandArgs = {
                "-jar",
                "openapi-generator/modules/openapi-generator-cli/target/openapi-generator-cli.jar",
                "generate",
                "-i",
                "openapi.yaml",
                "-g",
                "typescript-fetch",
                "-o",
                outputDir,
                "-c",
                "openapi-generator.json",
                "--reserved-words-mappings",
                "delete=delete"
        };

        try {
            String[] fullCommand = new String[commandArgs.length + 1];
            fullCommand[0] = command;
            System.arraycopy(commandArgs, 0, fullCommand, 1, commandArgs.length);

            ProcessBuilder builder = new ProcessBuilder(fullCommand);
            Process process = builder.start();

            printOutput(process.getInputStream(), "stdout");
            printOutput(process.getErrorStream(), "stderr");

            int exitCode = process.waitFor();
            System.out.println("Command exited with code " + exitCode);
        } catch (IOException | InterruptedException e) {
            e.printStackTrace();
        }
    }

    private static void deleteDirectory(File directory) {
        if (directory.exists()) {
            File[] files = directory.listFiles();
            if (files != null) {
                for (File file : files) {
                    if (file.isDirectory()) {
                        // Recurse
                        deleteDirectory(file);
                    } else {
                        // Delete file
                        file.delete();
                    }
                }
            }
            directory.delete();
        }
    }

    private static void printOutput(java.io.InputStream inputStream, String type) throws IOException {
        java.io.BufferedReader reader = new java.io.BufferedReader(new java.io.InputStreamReader(inputStream));
        String line;
        while ((line = reader.readLine()) != null) {
            System.out.println(type + ": " + line);
        }
    }
}