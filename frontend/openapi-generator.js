import { spawn } from 'child_process';
import fs from 'fs';

let outputDir = 'cmsTypeScriptClient';

// Function to delete directory recursively
function deleteDirectory(path) {
	if (fs.existsSync(path)) {
		fs.readdirSync(path).forEach((file) => {
			const curPath = path + '/' + file;
			if (fs.lstatSync(curPath).isDirectory()) {
				// Recurse
				deleteDirectory(curPath);
			} else {
				// Delete file
				fs.unlinkSync(curPath);
			}
		});
		fs.rmdirSync(path);
	}
}

deleteDirectory(outputDir);

// Run the openapi-generator-cli command

const command = 'C:/Program Files/nodejs/npx.cmd';
const args = [
	'@openapitools/openapi-generator-cli',
	'generate',
	'-i',
	'openapi.yaml',
	'-g',
	'typescript-fetch',
	'-o',
	outputDir,
	'-c',
	'openapi-generator.json',
	'--reserved-words-mappings',
	'delete=delete'
];

const generatorProcess = spawn(command, args);

generatorProcess.stdout.on('data', (data) => {
	console.log(`stdout: ${data}`);
});

generatorProcess.stderr.on('data', (data) => {
	console.error(`stderr: ${data}`);
});

generatorProcess.on('error', (error) => {
	console.error(`error: ${error.message}`);
});

generatorProcess.on('close', (code) => {
	console.log(`Command exited with code ${code}`);
});
