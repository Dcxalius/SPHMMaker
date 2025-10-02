# Resolving `glCreateShader` and other OpenGL entry-point errors

Compilers that target the Windows OpenGL 1.1 headers only expose the fixed function API.  
Modern entry points such as `glCreateShader`, `glShaderSource`, `glCompileShader`, and the buffer or vertex array helpers are extensions that have to be queried at runtime.  
If no loader is used you will see build errors such as:

```
'glCreateShader': identifier not found
'glShaderSource': identifier not found
'glCompileShader': identifier not found
...
```

To fix the build you have to load the modern OpenGL symbols before using them.  There are two common approaches:

## Use an OpenGL loader library

1. Add a loader such as [GLAD](https://glad.dav1d.de/) or [GLEW](http://glew.sourceforge.net/) to the project.
2. Include the generated loader header in every translation unit that uses modern OpenGL symbols:
   ```cpp
   #include <glad/glad.h>
   #include <GLFW/glfw3.h>
   ```
   or, if you use GLEW:
   ```cpp
   #include <GL/glew.h>
   ```
3. Initialize the loader immediately after creating the OpenGL context:
   ```cpp
   if (!gladLoadGLLoader((GLADloadproc)glfwGetProcAddress)) {
       throw std::runtime_error("Failed to load OpenGL functions");
   }
   ```
   (For GLEW use `glewInit()` instead.)

Once the loader is initialized the compiler can resolve the OpenGL functions that caused the errors.

## Manually load the function pointers

If you cannot add an external dependency you can declare the procedure types yourself and fetch the addresses by using `wglGetProcAddress` after the context has been created:

```cpp
PFNGLCREATESHADERPROC glCreateShader = nullptr;
PFNGLSHADERSOURCEPROC glShaderSource = nullptr;
PFNGLCOMPILESHADERPROC glCompileShader = nullptr;

void LoadGLFunctions()
{
    glCreateShader = reinterpret_cast<PFNGLCREATESHADERPROC>(wglGetProcAddress("glCreateShader"));
    glShaderSource = reinterpret_cast<PFNGLSHADERSOURCEPROC>(wglGetProcAddress("glShaderSource"));
    glCompileShader = reinterpret_cast<PFNGLCOMPILESHADERPROC>(wglGetProcAddress("glCompileShader"));
    // Load any other required functions here...
}
```

Call `LoadGLFunctions()` once the OpenGL context is current.  After that the modern functions can be used without compile or link errors.

Both strategies ensure the project has access to the OpenGL 2+ entry points that are missing from the default Windows headers, eliminating the "identifier not found" errors during compilation.
